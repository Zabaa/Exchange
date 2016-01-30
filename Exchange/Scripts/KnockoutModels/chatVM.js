function chatMessageViewModel(id, conversationId, message, date) {
    var self = this;
    self.Id = id;
    self.ConversationId = conversationId;
    self.Message = message;
    self.Date = date;
}

function chatConversation(id, chatId, recipientId, messages) {
    var self = this;
    self.Id = id;
    self.ChatId = chatId;
    self.RecipientId = recipientId;
    self.Messages = ko.observableArray(messages);
}

function chatContact(recipientId, status) {
    var self = this;
    self.recipientId = recipientId;
    self.status = status;
}


function chatViewModel(data) {
    var self = this;

    self.Id = ko.observable();
    self.ContactList = ko.observableArray();
    self.Conversations = ko.observableArray();

    self.load = function (dataJson) {
        debugger;
        if (dataJson) {
            ko.mapping.fromJSON(dataJson, {}, self);
        }
    }
    self.load(data);
}