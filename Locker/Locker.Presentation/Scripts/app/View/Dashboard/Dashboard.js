$(document).ready(function () {
    var dashboardViewModel = function () {
        var self = this;

        self.Blocks = ko.observableArray();

        self.filter = ko.observable();

        self.filteredList = ko.computed(function () {
            var filter = self.filter(),
                arr = [];
            if (filter) {
                ko.utils.arrayForEach(self.Blocks(), function (item) {
                    if (item.LockerBlockId.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.SectorName.toString().toUpperCase() == filter.toString().toUpperCase()                    ) {

                        arr.push(item);
                    } 
                });
            } else {
                arr = self.Blocks();
            }
            return arr;
        });

        function GetAllLockers() {
            $.ajax({
                type: 'GET',
                url: '/Dashboard/GetAllLockerByLockerBlocks/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));
                        self.Blocks(response);
                        console.log(self.Blocks());
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