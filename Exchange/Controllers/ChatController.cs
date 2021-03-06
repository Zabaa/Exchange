﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Abstract.Services;
using Exchange.Domain.Chat;
using Exchange.Hubs.Chat;
using Exchange.ViewModel.Chat;
using Mapster;
using Microsoft.AspNet.Identity;
using NLog;
using NLog.Fluent;

namespace Exchange.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IConversationService _conversationService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ChatController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public ActionResult Index()
        {
            var conversations = _conversationService.GetConversations(User.Identity.GetUserId());
            var conversationViewModel =
                TypeAdapter.Adapt<IEnumerable<Conversation>, IEnumerable<ConversationViewModel>>(conversations);
            var chatViewModel = new ChatViewModel
            {
                Conversations = conversationViewModel,
                CurrentUserId = User.Identity.GetUserId()
            };

            return View(chatViewModel);
        }

        [HttpPost]
        public JsonResult AddMessage(MessageViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            var message = TypeAdapter.Adapt<MessageViewModel, Message>(viewModel);

            try
            {
                _conversationService.AddMessage(message);
                var conversation = _conversationService.GetConversation(message.ConversationId);
                viewModel.Id = message.Id;

                ChatMonitor.Instance.SendMessage(conversation.SenderId, conversation.RecipientId, viewModel);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult AddConverstion(string recipientId)
        {
            try
            {
                var conversation = new Conversation
                {
                    RecipientId = recipientId,
                    SenderId = User.Identity.GetUserId()
                };

                _conversationService.AddConversation(conversation);

                if (conversation.Id != default(int))
                {
                    conversation = _conversationService.GetConversation(conversation.Id);
                    var conversationViewModel = TypeAdapter.Adapt<Conversation, ConversationViewModel>(conversation);
                    ChatMonitor.Instance.AddConversation(conversationViewModel);
                }
                    
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return Json(new { success = false });
            }
        }
    }
}