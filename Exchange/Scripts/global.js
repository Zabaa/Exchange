$(function () {
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        }
    });
});

function initializeDatePicker() {
    $('.datepicker').datepicker({
        format: "yyyy-mm-dd",
        startDate: "this",
        clearBtn: true,
        language: "pl",
        autoclose: true
    });
};

function scrollToBottom(elementId) {
    var object = document.getElementById(elementId);
    object.scrollTop = object.scrollHeight;
};

function scrollToBottomByClassName(className) {
    var object = $("div.tab-pane.active").find("." + className);
    var scrollHeight = object.prop("scrollHeight");
    object.prop("scrollTop", scrollHeight);
};

function scrollToBottomAnimate(elementId) {
    var object = $("#" + elementId);
    object.animate({ scrollTop: object.prop("scrollHeight") }, 1000);
};

function scrollToBottomAnimateByClassName(className) {
    var auctionOffers = $("div.tab-pane.active").find("." + className);
    auctionOffers.animate({ scrollTop: auctionOffers.prop("scrollHeight") }, 1000);
};
