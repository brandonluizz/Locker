$(document).ready(function () {
    var dashboardViewModel = function () {
        var self = this;

        self.Lockers = ko.observableArray();
        self.Teste = ko.observable(true);

        function GetAllLockers() {

            $.ajax({
                type: 'GET',
                url: '/Dashboard/GetAllLockerBlocks/',
                success: function (responseLockerBlock) {
                    if (responseLockerBlock) {
                        debugger;
                        var lockerBlocks = $.parseJSON(JSON.stringify(responseLockerBlock));
                        $.ajax({
                            type: 'GET',
                            url: '/Dashboard/GetAllLockers/',
                            success: function (responseLocker) {
                                if (responseLocker) {
                                    debugger;
                                    var lockers = $.parseJSON(JSON.stringify(responseLocker));
                                    GenerateLockersWithLockerBlock(lockerBlocks, lockers);
                                    self.Lockers(lockers);
                                    console.log(self.Lockers());
                                }
                            }
                        });
                    }
                }
            });
        }

        function GenerateLockersWithLockerBlock(lockerBlocks, lockers) {
            var teste = [{

            }];
            for (var index = 0; index < lockerBlocks.length; index++) {

                for (var i = 0; i < lockers.length; i++) {
                    lockerBlocks[index].Lockers.push(lockers[i]);
                }
            }
        }


        self.InitializeComponent = function () {
            GetAllLockers();
        }();
    };

    ko.applyBindings(new dashboardViewModel());
});