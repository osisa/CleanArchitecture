﻿using AdminDashboard.Wasm.Models;
using BlazorHero.CleanArchitecture.Application.Models.Chat;
using BlazorHero.CleanArchitecture.Application.Responses.Identity;
using BlazorHero.CleanArchitecture.Client.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Pages.Communication
{
    public partial class Chat
    {
        private HubConnection hubConnection;
        [Parameter] public string CurrentMessage { get; set; }
        private bool isConnected => hubConnection.State == HubConnectionState.Connected;
        private List<ChatHistory> messages = new List<ChatHistory>();
        private class MessageRequest
        {
            public string userName { get; set; }
            public string message { get; set; }
        }
        MessageRequest model = new MessageRequest();
        private async Task SubmitAsync()
        {
            if(!string.IsNullOrEmpty(CurrentMessage) && !string.IsNullOrEmpty(CId))
            {               
                //Save Message to DB
                var chatHistory = new ChatHistory()
                {
                    Message = CurrentMessage,
                    ToUserId = CId,
                    CreatedDate = DateTime.Now

                };
                var response = await _chatManager.SaveMessageAsync(chatHistory);
                if (response.Succeeded)
                {
                    var state = await _stateProvider.GetAuthenticationStateAsync();
                    var user = state.User;
                    var UserId = user.GetUserId();
                    var userName = $"{user.GetFirstName()} {user.GetLastName()}";
                    await hubConnection.SendAsync("SendMessageAsync", userName, CurrentMessage);
                    CurrentMessage = string.Empty;
                }
                else
                {
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(localizer[message], Severity.Error);
                    }
                }

            }            
        }
        private async Task OnKeyPressInChat(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await SubmitAsync();
            }
        }
        protected override async Task OnInitializedAsync()
        {         
           
            hubConnection = new HubConnectionBuilder().WithUrl(_navigationManager.ToAbsoluteUri("/chatHub"))
            .Build();
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                messages.Add(new ChatHistory { Message = message, FromUserId = user });
                StateHasChanged();
            });
            await hubConnection.StartAsync();
            await GetUsersAsync();
        }
        public List<ChatUserResponse> UserList = new List<ChatUserResponse>();
        [Parameter] public string CFullName { get; set; }
        [Parameter] public string CId { get; set; }
        [Parameter] public string CUserName { get; set; }
        async Task LoadUserChat(string userId)
        {
            var response = await _userManager.GetAsync(userId);        
            if (response.Succeeded)
            {
                var contact = response.Data;
                CId = contact.Id;
                CFullName = $"{contact.FirstName} {contact.LastName}";
                CUserName = contact.UserName;
                _navigationManager.NavigateTo($"chat/{CUserName}");
                //Load messages from db here
                messages = new List<ChatHistory>();
                var historyResponse = await _chatManager.GetChatHistoryAsync(CId);
                if(historyResponse.Succeeded)
                {
                    messages = historyResponse.Data.ToList();
                }
                else
                {
                    foreach (var message in historyResponse.Messages)
                    {
                        _snackBar.Add(localizer[message], Severity.Error);
                    }
                }

            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(localizer[message], Severity.Error);
                }
            }
            
        }
        private async Task GetUsersAsync()
        {
            //add get chat history from chat controller / manager
            var response = await _chatManager.GetChatUsersAsync();
            if (response.Succeeded)
            {
                UserList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(localizer[message], Severity.Error);
                }
            }
        }

        bool open;
        Anchor ChatDrawer { get; set; }
        void OpenDrawer(Anchor anchor)
        {
            ChatDrawer = anchor;
            open = true;
        }
    }
}
