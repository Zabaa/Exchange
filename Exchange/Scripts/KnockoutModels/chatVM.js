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

    self.submitMessage = function (formElement) {
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

    self.loadMessages = function (messagesToLoad) {
        if (messagesToLoad) {
            self.Messages.removeAll();
            ko.utils.arrayForEach(messagesToLoad, function (message) {
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

function chatContact(id, name) {
    var self = this;
    self.Id = id;
    self.Name = name;
}

function chatViewModel(data, addMessageUrl) {
    var self = this;

    self.ContactList = ko.observableArray();
    self.Conversations = ko.observableArray();
    self.CurrentUserId = ko.observable();

    self.addMessage = function (message) {
        var observableMessage = ko.mapping.fromJS(message);
        var conversation = ko.utils.arrayFirst(self.Conversations(), function(item) {
            return item.Id === observableMessage.ConversationId();
        });

        if (conversation) {
            conversation.addMessage(observableMessage);
        }
    };

    self.loadContacts = function (contacts) {
        if (contacts) {
            self.AuctionOffers.removeAll();
            ko.utils.arrayForEach(contacts, function (contact) {
                self.ContactList.push(new chatContact(contact.RecipientId(), contact.RecipientName()));
            });
        }
    }

    self.addContact = function(contact) {
        if (!contact)
            return;
        self.ContactList.push(new chatContact(contact.Id, contact.Name));
    }

    self.removeContact = function(contact) {
        if (!contact)
            return;
        self.ContactList.remove(function(item) {
            return item.Name === contact.Name && item.Id === contact.Id;
        });
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