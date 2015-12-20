$(function () {
    debugger;
    ko.applyBindings(AuctionViewModel);
});

function AuctionHistory(userId, userName, date, price, timelineInverted) {
    var self = this;

    self.userId = userId;
    self.userName = userName;
    self.date = date;
    self.price = ko.observable(price);
    self.timelineInverted = timelineInverted;
}

var AuctionViewModel = function() {
    var self = this;

    self.auctionHistory = ko.observableArray([
        new AuctionHistory('147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Łukasz Z', '2015-12-14 21:47:43', '100.00', false),
        new AuctionHistory('147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Piotr K', '2015-12-15 21:47:43', '120.00', true),
        new AuctionHistory('147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Łukasz Z', '2015-12-16 21:47:43', '125.00', false),
        new AuctionHistory('147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Ilona K', '2015-12-16 21:49:06', '130.00', true),
        new AuctionHistory('147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Łukasz Z', '2015-12-17 21:49:06', '150.00', false)
    ]);

    self.addHistory = function() {
        
    }
}

