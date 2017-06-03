$(document).ready(function () {
    var dashboardViewModel = function () {
        var self = this;

        self.Blocks = ko.observableArray();

        function GetAllLockers() {
            $.ajax({
                type: 'GET',
                url: '/Dashboard/GetAllLockerByLockerBlocks/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));
                        self.Blocks(response);
                        //console.log(self.Blocks());
                    }
                }
            });
        }

        setInterval(GetAllLockers, 60000);

        self.InitializeComponent = function () {
            GetAllLockers();
        }();
    };

    ko.applyBindings(new dashboardViewModel());
});