$(document).ready(function () {
    var dashboardViewModel = function () {
        var self = this;

        self.Lockers = ko.observableArray();
        self.Teste = ko.observable(true);

        function GetAllLockers() {
            $.ajax({
                type: 'GET',
                url: '/Dashboard/GetAllLockers/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        self.Lockers(response);
                    }
                }
            });
        }

        self.InitializeComponent = function () {
            GetAllLockers();
        }();
    };

    ko.applyBindings(new dashboardViewModel());
});