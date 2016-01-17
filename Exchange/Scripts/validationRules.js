function ValidationRules(viewModel) {
    function validateInteger(value, element, param) {
        if (!value) {
            return false;
        }
        var parsedNumber = parseFloat(value);
        if (isNaN(parsedNumber)) {
            return false;
        } else {
            return (value == parsedNumber);
        }
    };

    function validateBiggestPrice(value, element, param) {
        if (!value) {
            return false;
        }
        return value > viewModel.getBiggestPrice();
    };

    this.defineValidationRules = function() {
        $.validator.addMethod("offerPrice", $.validator.methods.required, "Należy podać cene");
        $.validator.addMethod("offerNumber", validateInteger, "Cena musi być liczbą");
        $.validator.addMethod("offerMinPrice", $.validator.methods.min, $.format("Cena musi być większa niż {0} PLN"));
        $.validator.addMethod("offerBiggestPrice", validateBiggestPrice, "Cena musi być większa niż cena poprzedniej oferty");

        $.validator.addClassRules("validate-offerPrice", { offerPrice: true, offerNumber: true, offerMinPrice: 0, offerBiggestPrice: true });
    };
};
