$(document).ready(function () {

    var sectorViewModel = function () {
        var self = this;

        self.SectorName = ko.observable('');
        self.SectorLocation = ko.observable('');
        self.SectorLocations = ko.observableArray();
        self.SelectedSectorLocation = ko.observable();
        self.NewSectorAddedWithSuccess = ko.observable(false);
        self.NewSectorAddedWithError = ko.observable(false);
        self.Lockers = ko.observableArray();

        self.SectorSubmit = function (formElement) {
            if (self.SectorFormIsValid()) {
                $.post('/LockerSetup/AddNewSector/', $(formElement).serialize(), SubmittedWithSuccess, 'json');
            }
        }

        self.OpenNewSectorModal = function () {
            self.CleanSectorHtmlValues();
            $('#sector-modal').modal('show');
        }

        self.SectorFormIsValid = function () {
            if (self.SectorName === '') { return false; }
            if (self.SectorLocation === '') { return false; }
            return true;
        }

        self.CleanSectorHtmlValues = function () {
            self.SectorName('');
            self.NewSectorAddedWithSuccess(false);
            self.NewSectorAddedWithError(false);
        }

        function GetAllLockers() {
            $.ajax({
                type: 'GET',
                url: '/LockerSetup/GetAllLockers/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        self.Lockers(response);
                        console.log(self.Lockers());
                    }
                }
            });
        }

        function GetSectorLocations() {
            $.ajax({
                type: 'GET',
                url: '/LockerSetup/GetSectorLocations/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        for (var i = 0; i < response.length; i++) {
                            var sectorLocations = new DropDown(response[i].SectorLocationId, response[i].SectorLocationName);
                            self.SectorLocations.push(sectorLocations);
                        }

                        self.SelectedSectorLocation(self.SectorLocation);
                    }
                }
            });
        };

        function SubmittedWithSuccess(data) {
            if (data.Success) {
                self.NewSectorAddedWithSuccess(true);
            } else {
                self.NewSectorAddedWithError(true);
            }
        }

        self.InitializeComponent = function () {
            GetSectorLocations();
            GetAllLockers();
        }();
    };

    var blockViewModel = function () {
        var self = this;

        self.TotalNumberOfVerticalLockers = ko.observable('');
        self.TotalNumberOfHorizontalLockers = ko.observable('');
        self.BlockAddedWithSuccess = ko.observable(false);
        self.BlockError = ko.observable(false);
        self.Sectors = ko.observableArray();
        self.SelectedSector = ko.observable('');
        self.Sector = ko.observable('');

        self.LockerBlockSubmit = function (formData) {
            if (self.BlockFormIsValid()) {
                $.post('/LockerSetup/AddNewLockerBlock/', $(formData).serialize(), SubmittedWithSuccess, 'json');
            }
        }

        self.BlockFormIsValid = function () {
            if (self.TotalNumberOfVerticalLockers === '') { return false; }
            if (self.TotalNumberOfHorizontalLockers === '') { return false; }

            return true;
        }


        function GetSectors() {
            $.ajax({
                type: 'GET',
                url: '/LockerSetup/GetAllSectors/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        for (var i = 0; i < response.length; i++) {
                            var sectors = new DropDown(response[i].SectorId, response[i].SectorName);
                            self.Sectors.push(sectors);
                        }

                        self.SelectedSector(self.Sector);
                    }
                }
            });
        };

        function SubmittedWithSuccess(data) {
            if (data.Success) {
                self.BlockAddedWithSuccess(true);
            } else {
                self.BlockError(true);
            }
        }

        self.InitializeComponent = function () {
            GetSectors();
        }();
    };

    var lockerViewModel = function () {
        var self = this;


        self.LockerBlockers = ko.observableArray();
        self.SelectLockerBlock = ko.observable('');
        self.LockerBlock = ko.observable('');
        self.NewLockerAddedWithSuccess = ko.observable(false);
        self.NewLockerError = ko.observable(false);
        self.VerticalPositionNumber = ko.observable('');
        self.HorizontalPositionNumber = ko.observable('');
        self.IsAvailableLockerBlock = ko.observable(false);
        self.HaveBlankFields = ko.observable(false);

        self.LockerSubmit = function (formElement) {
            //TODO validar form
            if (IsValidForm()) {
                if (ValidadeIsAvailableLockerBlock()) {
                    $.post('/LockerSetup/AddNewLocker/', $(formElement).serialize(), SubmittedWithSuccess, 'json');
                }
            }
        }

        function IsValidForm() {
            if (self.VerticalPositionNumber()) { return true; }
            if (self.HorizontalPositionNumber()) { return true; }
            if (self.LockerBlock()) { return true; }

            self.HaveBlankFields(true);

            return false;
        }

        function ValidadeIsAvailableLockerBlock() {
            $.ajax({
                type: 'POST',
                data: { 'lockerBlockId': self.SelectLockerBlock() },
                url: '/LockerSetup/IsAvailableLockerBlock/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        self.IsAvailableLockerBlock(data.Success);
                        return data.Success;
                    }
                }
            });
        }

        function GetAllLockerBlocks() {
            $.ajax({
                type: 'GET',
                url: '/LockerSetup/GetAllLockerBlocks/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        for (var i = 0; i < response.length; i++) {
                            var lockerBlockers = new DropDown(response[i].LockerBlockId, response[i].LockerBlockId);
                            self.LockerBlockers.push(lockerBlockers);
                        }

                        self.SelectLockerBlock(self.LockerBlock);
                    }
                }
            });
        };


        function SubmittedWithSuccess(data) {
            if (data.Success) {
                self.NewLockerAddedWithSuccess(true);
                self.NewLockerError(false);
            } else {
                self.NewLockerAddedWithSuccess(false);
                self.NewLockerError(true);
            }
        }

        self.InitializeComponent = function () {
            GetAllLockerBlocks();
        }();
    };

    function DropDown(value, description) {
        this.value = value;
        this.description = description;
    }

    $('#open-block-modal').click(function () {
        $('#block-modal').modal('show');
    });

    $('#open-locker-modal').click(function () {
        $('#locker-modal').modal('show');
    });

    $('#open-sector-modal').click(function () {
        $('#sector-modal').modal('show');
    });

    var sectorForm = document.getElementById('sector-form');
    ko.applyBindings(new sectorViewModel(), sectorForm);

    var blockForm = document.getElementById('locker-block-div');
    ko.applyBindings(new blockViewModel(), blockForm);

    var lockerForm = document.getElementById('locker-div');
    ko.applyBindings(new lockerViewModel(), lockerForm);
});