function AuctionOffer(auctionId, userId, userName, date, price, timelineInverted) {
    var self = this;
    self.AuctionId = auctionId;
    self.UserId = userId;
    self.UserName = userName;
    self.Date = date;
    self.Price = price;
    self.TimelineInverted = timelineInverted;
}

function AuctionViewModel(data, addOfferUrl) {
    var self = this;
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.Description = ko.observable();
    self.OpenPrice = ko.observable();
    self.StartDate = ko.observable();
    self.PredictedEndDate = ko.observable();
    self.PriceToOffer = ko.observable();
    self.UserId = ko.observable();
    self.UserName = ko.observable();

    self.AuctionOffers = ko.observableArray([
        new AuctionOffer(1, '147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Łukasz Z', '2015-12-14 21:47:43', '100.00', false),
        new AuctionOffer(1, '147b92ec-0e72-4265-bcd8-a4eb72c5d72c', 'Piotr K', '2015-12-15 21:47:43', '120.00', true)
    ]);

    self.TimelineInverted = ko.computed(function () {
        return !(self.AuctionOffers().length % 2 === 0);
    });

    self.newOffersEnable = ko.computed(function() {
        return new Date(self.PredictedEndDate()) > new Date();
    });

    self.endOfAuction = ko.computed(function () {
        return new Date(self.PredictedEndDate()) < new Date();
    });

    self.soonEndOfAuction = ko.computed(function() {
        if (self.endOfAuction()) {
            return false;
        }
        var date = new Date();
        date.setDate(date.getDate() + 1);
        return new Date(self.PredictedEndDate()) < date;
    });

    self.getBiggestPrice = function() {
        var result = Math.max.apply(null, ko.utils.arrayMap(self.AuctionOffers(), function (offer) {
            return offer.Price;
        }));
        return result;
    };

    self.submitOffer = function(formElement) {
        $(formElement).validate();
        if (!$(formElement).valid()) {
            return false;
        }

        var actualDateTime = new Date().toISOString();
        var offer = new AuctionOffer(self.Id(), self.UserId(), self.UserName(), actualDateTime, self.PriceToOffer(), false);

        $.ajax({
            url: addOfferUrl,
            type: "POST",
            cache: false,
            data: offer,
            success: function(result) {
                if (result.success) {
                    console.log("Oferta dodana");
                } else if (result.success === false) {
                    alert("Wystąpił błąd");
                } else {
                    alert("Wystąpił błąd");
                }
            }
        });
        return false;
    };

    self.addOffer = function (offer) {
        self.AuctionOffers.push(new AuctionOffer(offer.AuctionId, offer.UserId, offer.UserName, offer.Date, offer.Price, self.TimelineInverted()));
    }

    self.loadOffers = function (offers) {
        if (offers) {
            self.AuctionOffers.removeAll();
            ko.utils.arrayForEach(offers, function(offer) {
                self.AuctionOffers.push(new AuctionOffer(offer.AuctionId(), offer.UserId(), offer.UserName, offer.Date(), offer.Price(), self.TimelineInverted()));
            });
        }
    }

    self.load = function (dataJson) {
        debugger;
        if (dataJson) {
            ko.mapping.fromJSON(dataJson, {}, self);
            var dataJs = ko.mapping.fromJSON(dataJson);
            self.loadOffers(dataJs.AuctionOffers());
        }
    }

    self.load(data);
}