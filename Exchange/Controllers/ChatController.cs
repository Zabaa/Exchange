using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Abstract.Services;
using Exchange.Domain.Chat;
using Exchange.ViewModel.Chat;
using Mapster;
using Microsoft.AspNet.Identity;

namespace Exchange.Controllers
{
    public class ChatController : Controller
    {
        private readonly IConversationService _conversationService;

        public ChatController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        public ActionResult Index()
        {
            var conversations = _conversationService.GetConversations(User.Identity.GetUserId());
            var conversationViewModel =
                TypeAdapter.Adapt<IEnumerable<Conversation>, IEnumerable<ConversationViewModel>>(conversations);

            return View(conversationViewModel);
        }
    }
}