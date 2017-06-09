$(document).ready(function () {

    var sectorViewModel = function () {
        var self = this;

        self.SectorName = ko.observable('');
        self.SectorLocation = ko.observable('');
        self.SectorLocations = ko.observableArray();
        self.SelectedSectorLocation = ko.observable();
        self.NewSectorAddedWithSuccess = ko.observable(false);
        self.NewSectorAddedWithError = ko.observable(false);
        self.HaveBlankFields = ko.observable(false);

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
            if (self.SectorName() === '') {
                self.HaveBlankFields(true);
                return false;
            }
            if (self.SectorLocation() === '') {
                self.HaveBlankFields(true);
                return false;
            }

            self.HaveBlankFields(false);
            return true;
        }

        self.CleanSectorHtmlValues = function () {
            self.SectorName('');
            self.NewSectorAddedWithSuccess(false);
            self.NewSectorAddedWithError(false);
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
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'O setor foi adicionado!',
                    position: 'topRight'
                });
            } else {
                self.NewSectorAddedWithError(true);
            }
        }

        self.InitializeComponent = function () {
            GetSectorLocations();
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
        self.Lockers = ko.observableArray();
        self.IsHaveBlankField = ko.observable(false);

        self.filter = ko.observable();

        self.filteredList = ko.computed(function () {
            var filter = self.filter(),
                arr = [];
            if (filter) {
                ko.utils.arrayForEach(self.Lockers(), function (item) {
                    if (item.LockerId.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.LockerBlock.Sector.SectorName.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.LockerBlockId.toString().toUpperCase() == filter.toString().toUpperCase()) {
                        debugger;
                        arr.push(item);
                    }

                });
            } else {
                arr = self.Lockers();
            }
            return arr;

        });

        //LockerData
        self.ArduinoDataErrorMessage = ko.observable(false);
        self.ArduinoCodeError = ko.observable(false);
        self.LockerId = ko.observable('');
        self.NumberOfPositionLocker = ko.observable('');
        self.HorizontalPositionNumber = ko.observable('');
        self.VerticalPositionNumber = ko.observable('');
        self.ArduinoId = ko.observable('');
        
        self.AddArduinoCodeToLocker = function (formData) {
            if (self.ArduinoId() != '') {
                $.post('/LockerSetup/AddArduinoDataInLocker/', $(formData).serialize(), AddArduinoDataInLockerSuccess, 'json');
            } else {
                self.ArduinoDataErrorMessage(true);
            }
        }

        self.EditLocker = function (locker) {
            self.LockerId(locker.LockerId);
            self.NumberOfPositionLocker(locker.NumberOfPositionLocker);
            self.HorizontalPositionNumber(locker.HorizontalPositionNumber);
            self.VerticalPositionNumber(locker.VerticalPositionNumber);
            self.ArduinoId(locker.ArduinoId);

            $('#locker-modal').modal('show');
        }

        self.LockerBlockSubmit = function (formData) {
            if (self.BlockFormIsValid()) {
                $.post('/LockerSetup/AddNewLockerBlock/', $(formData).serialize(), SubmittedWithSuccess, 'json');
            }
        }

        self.BlockFormIsValid = function () {
            if (self.TotalNumberOfVerticalLockers() === '') {
                self.IsHaveBlankField(true);
                return false;
            }

            if (self.TotalNumberOfHorizontalLockers() === '') {
                self.IsHaveBlankField(true);
                return false;
            }

            if (self.TotalNumberOfHorizontalLockers() === '') {
                self.IsHaveBlankField(true);
                return false;
            }

            self.IsHaveBlankField(false);
            return true;
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

        function AddArduinoDataInLockerSuccess(data) {
            if (data.Success) {
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'O Código do Arduino desse armário foi cadastrado!',
                    position: 'topRight'
                });
                self.ArduinoDataErrorMessage(false);
                GetAllLockers();
            } else {
                self.ArduinoCodeError(true);
            }
        }

        function SubmittedWithSuccess(data) {
            if (data.Success) {
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'O bloco de armário foi construido!',
                    position: 'topRight'
                });
                GetAllLockers();
                document.getElementById("locker-block-form").reset();
            } else {
                self.BlockError(true);
            }
        }

        self.InitializeComponent = function () {
            GetSectors();
            GetAllLockers();
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
        self.IsUnavailableLockerBlock = ko.observable(false);
        self.HaveBlankFields = ko.observable(false);
        self.IsUnavailableLockerPosition = ko.observable(false);

        self.LockerSubmit = function (formElement) {
            //TODO validar form
            if (IsValidForm()) {
                ValidateIsAvailableLockerBlockAndSubmit(formElement);
            }
        }

        function IsValidForm() {
            if (self.VerticalPositionNumber() === '') { self.HaveBlankFields(true); return false; }
            if (self.HorizontalPositionNumber() === '') { self.HaveBlankFields(true); return false; }
            if ($('#LockerBlock').val() === '') { self.HaveBlankFields(true); return false; }

            self.HaveBlankFields(false);

            return true;
        }

        function ValidateIsAvailableLockerBlockAndSubmit(formElement) {
            $.ajax({
                type: 'POST',
                data: { 'lockerBlockId': self.SelectLockerBlock() },
                url: '/LockerSetup/IsAvailableLockerBlock/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));

                        self.IsUnavailableLockerBlock(data.Success == false);

                        if (data.Success) {
                            $.ajax({
                                type: 'POST',
                                data: {
                                    "LockerPosition":
                                    {
                                        "LockerBlockId": self.LockerBlock,
                                        "HorizontalPositionNumber": self.HorizontalPositionNumber,
                                        "VerticalPositionNumber": self.VerticalPositionNumber
                                    }
                                },
                                url: '/LockerSetup/IsAvailableLockerPosition/',
                                success: function (data) {
                                    if (data) {
                                        var response = $.parseJSON(JSON.stringify(data));

                                        if (data.Success) {
                                            $.post('/LockerSetup/AddNewLocker/', $(formElement).serialize(), SubmittedWithSuccess, 'json');
                                        } else {
                                            self.IsUnavailableLockerPosition(true);
                                        }
                                    }
                                }
                            });
                        }
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
                self.IsUnavailableLockerBlock(false);
                self.HaveBlankFields(false);
                self.IsUnavailableLockerPosition(false);
            } else {
                self.NewLockerAddedWithSuccess(false);
                self.NewLockerError(true);
                self.NewLockerAddedWithSuccess(false);
            }
        }

        self.InitializeComponent = function () {
            GetAllLockerBlocks();
        }();
    };

    var sectorLocationViewModel = function () {
        var self = this;

        self.SectorLocationName = ko.observable('');
        self.IsHaveBlankFields = ko.observable(false);
        self.AddedWithSuccess = ko.observable(false);

        self.SectorLocationSubmit = function (formElement) {
            if (self.IsValidForm()) {
                $.post('/LockerSetup/AddNewSectorLocation/', $(formElement).serialize(), SectorLocationAddedWithSuccess, 'json');
            }
        }

        self.IsValidForm = function () {
            if (self.SectorLocationName() === '') {
                self.IsHaveBlankFields(true);
                return false;
            }

            self.IsHaveBlankFields(false);
            return true;
        }

        function SectorLocationAddedWithSuccess(data) {
            if (data.Success) {
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'A localização foi adicionada!',
                    position: 'topRight'
                });
            }

        }
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

    $('#open-sector-location-modal').click(function () {
        $('#sector-location-modal').modal('show');
    });

    var sectorLocation = document.getElementById('sector-location-div');
    ko.applyBindings(new sectorLocationViewModel(), sectorLocation);

    var sectorForm = document.getElementById('sector-form');
    ko.applyBindings(new sectorViewModel(), sectorForm);

    var blockForm = document.getElementById('locker-block-div');
    ko.applyBindings(new blockViewModel(), blockForm);    
});