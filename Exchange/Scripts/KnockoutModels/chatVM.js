function chatMessageViewModel(id, conversationId, content, date) {
    var self = this;
    self.Id = id;
    self.ConversationId = conversationId;
    self.Content = content;
    self.Date = date;
}

function chatConversation(id, recipientId, messages) {
    var self = this;
    self.Id = id;
    self.RecipientId = recipientId;
    self.Messages = ko.observableArray(messages);
}

function chatContact(recipientId, status) {
    var self = this;
    self.RecipientId = recipientId;
    self.Status = status;
}


function chatViewModel(data) {
    var self = this;

    self.ContactList = ko.observableArray();
    self.Conversations = ko.observableArray();

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
            self.AuctionOffers.removeAll();
            ko.utils.arrayForEach(conversations, function (conversation) {
                self.ContactList.push(new chatConversation(conversation.Id(), conversation.RecipientId(), conversation.Messages()));
            });
        }
    }

    self.load = function (dataJson) {
        debugger;
        if (dataJson) {
            ko.mapping.fromJSON(dataJson, {}, self);
            var dataJs = ko.mapping.fromJSON(dataJson);
            self.loadContacts(dataJs.ContactList());
            self.loadConversations(dataJs.Conversations());
        }
    }
    self.load(data);
}