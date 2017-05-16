define('DropDown',
    [

    ], function () {

        function DropDown(value, description) {
            this.value = value;
            this.description = description;
        }

        return {
            DropDown: DropDown
        }
    });