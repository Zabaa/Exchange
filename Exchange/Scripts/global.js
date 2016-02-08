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
    var objOffers = document.getElementById(elementId);
    objOffers.scrollTop = objOffers.scrollHeight;
};

function scrollToBottomAnimate(elementId) {
    var auctionOffers = $("#" + elementId);
    auctionOffers.animate({ scrollTop: auctionOffers.prop("scrollHeight") }, 1000);
};
