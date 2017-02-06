function stockOperation(price, operationDate) {
    var self = this;

    self.price = price;
    self.operationDate = operationDate;
}

function stockViewModel(symbol, price, dayOpen, change, percentChange, lastChangeDate) {
    var self = this;

    self.symbol = ko.observable(symbol);
    self.price = ko.observable(price);
    self.dayOpen = dayOpen;
    self.change = ko.observable(change);
    self.percentChange = ko.observable(percentChange);
    self.lastChangeDate = ko.observable(lastChangeDate);

    self.stockHistory = ko.observableArray();

    self.priceDisplay = ko.computed(function() {
        return self.price().toFixed(2);
    });

    self.dayOpenDisplay = ko.computed(function () {
        return self.dayOpen.toFixed(2);
    });

    self.changeDisplay = ko.computed(function() {
        var up = '▲', down = '▼';
        return (self.change() === 0 ? '' : self.change() >= 0 ? up : down ) + ' ' + self.change();
    });

    self.percentChangeDisplay = ko.computed(function() {
        return (self.percentChange() * 100).toFixed(2) + '%';
    });


    self.addOperation = function() {
        self.stockHistory.push(new stockOperation(
            self.price(),
            self.lastChangeDate()
        ));
    };
    self.addOperation();
}

function stockMonitorViewModel() {
    var self = this;

    self.stocks = ko.observableArray();

    self.clearStocks = function() {
        self.stocks.removeAll();
    };

    self.loadStocks = function (stocks) {
        self.stocks.removeAll();
        ko.utils.arrayForEach(stocks, function (stock) {
            self.stocks.push(new stockViewModel(
                stock.Symbol, stock.Price, stock.DayOpen,
                stock.Change, stock.PercentChange, stock.LastChangeDate));
        });
    };

    self.updateStockPrice = function(stock) {
        var stockToUpdate = ko.utils.arrayFirst(self.stocks(), function(item) {
            return item.symbol() === stock.Symbol;
        });
        
        if (stockToUpdate) {
            stockToUpdate.price(stock.Price);
            stockToUpdate.change(stock.Change);
            stockToUpdate.percentChange(stock.PercentChange);
            stockToUpdate.lastChangeDate(stock.LastChangeDate);
            stockToUpdate.addOperation();
        }
    };
}