function chatMessage(id, conversationId, content, date, isSender) {
    var self = this;
    self.Id = id;
    self.ConversationId = conversationId;
    self.Content = content;
    self.Date = date;
    self.IsSender = isSender;

    self.MessageSide = ko.computed(function() {
        return self.IsSender ? "left" : "right";
    });

    self.MessageContentSide = ko.computed(function () {
        return self.IsSender ? "pull-left" : "pull-right";
    });

    self.ImgSource = ko.computed(function() {
        return self.IsSender
            ? "http://placehold.it/50/55C1E7/fff&amp;text=U"
            : "http://placehold.it/50/FA6F57/fff&amp;text=ME";
    });
}

function chatConversation(id, currentUserId, recipientId, recipientName, senderId, senderName, messages, addMessageUrl) {
    var self = this;
    self.Id = id;
    self.CurrentUserId = currentUserId;
    self.RecipientId = recipientId;
    self.RecipientName = recipientName;
    self.SenderId = senderId;
    self.SenderName = senderName;
    self.Messages = ko.observableArray();
    self.MessagesContent = ko.observable();
    
    self.CurrentUserIsSender = ko.computed(function() {
        return self.CurrentUserId === self.SenderId;
    });

    self.addMessage = function(message) {
        self.Messages.push(new chatMessage(
            message.Id(),
            message.ConversationId(),
            message.Content(),
            message.Date(),
            message.IsSender()));
    }

    self.submitOffer = function (formElement) {
        $(formElement).validate();
        if (!$(formElement).valid()) {
            return false;
        }

        var actualDateTime = new Date().toISOString();
        var message = new chatMessage(null, self.Id, self.MessagesContent(), actualDateTime, self.CurrentUserIsSender());

        $.ajax({
            url: addMessageUrl,
            type: "POST",
            cache: false,
            data: message,
            success: function (result) {
                if (result.success) {
                    console.log("Wiadomość wysłana");
                } else if (result.success === false) {
                    alert("Wystąpił błąd");
                } else {
                    alert("Wystąpił błąd");
                }
            }
        });
        return false;
    }

    self.loadMessages = function (messages) {
        if (messages) {
            self.Messages.removeAll();
            ko.utils.arrayForEach(messages, function (message) {
                self.Messages.push(new chatMessage(
                    message.Id(),
                    message.ConversationId(),
                    message.Content(),
                    message.Date(),
                    message.IsSender()));
            });
        }
    }
    self.loadMessages(messages);
}

function chatContact(recipientId, status) {
    var self = this;
    self.RecipientId = recipientId;
    self.Status = status;
}


function chatViewModel(data, addMessageUrl) {
    var self = this;

    self.ContactList = ko.observableArray();
    self.Conversations = ko.observableArray();
    self.CurrentUserId = ko.observable();

    self.loadContacts = function (contacts) {
        if (contacts) {
            self.AuctionOffers.removeAll();
            ko.utils.arrayForEach(contacts, function (contact) {
                self.ContactList.push(new chatContact(contact.RecipientId(), contact.Status()));
            });
        }
    }

    self.loadConversations = function (conversations) {
        if (conversations) {
            self.Conversations.removeAll();
            ko.utils.arrayForEach(conversations, function (conversation) {
                self.Conversations.push(new chatConversation(
                    conversation.Id(),
                    self.CurrentUserId(),
                    conversation.RecipientId(),
                    conversation.RecipientName(),
                    conversation.SenderId(),
                    conversation.SenderName(),
                    conversation.Messages(),
                    addMessageUrl));
            });
        }
    }

    self.load = function (dataJson) {
        debugger;
        if (dataJson) {
            var dataJs = ko.mapping.fromJSON(dataJson);
            self.CurrentUserId(dataJs.CurrentUserId());
            self.loadConversations(dataJs.Conversations());
        }
    }
    self.load(data);
}