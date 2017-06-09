$(document).ready(function () {
    $('.open-new-customer-modal').click(function () {
        $('#new-customer-modal').modal('show');
    });

    var newCustomerViewModel = function () {
        var self = this;

        self.CustomerId = ko.observable('');
        self.CustomerName = ko.observable('');
        self.CustomerCpf = ko.observable('');
        self.BirthDate = ko.observable('');
        self.TagUID = ko.observable('');
        self.HaveABlankFields = ko.observable(false);
        self.Error = ko.observable(false);
        self.AlreadyExistsThisCustomer = ko.observable(false);
        self.Customers = ko.observableArray();
        self.filter = ko.observable();

        self.filteredList = ko.computed(function () {
            var filter = self.filter(),
                arr = [];
            if (filter) {
                ko.utils.arrayForEach(self.Customers(), function (item) {
                    if (item.CustomerId.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.CustomerName.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.CustomerCpf.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.FormattedBirthDate.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.FormattedRegistrationDate.toString().toUpperCase() == filter.toString().toUpperCase()
                        || item.TagUID.toString().toUpperCase() == filter.toString().toUpperCase()) {

                        arr.push(item);
                    }
                });
            } else {
                arr = self.Customers();
            }
            return arr;
        });

        //edit user
        self.EditCustomerId = ko.observable('');
        self.EditCustomerName = ko.observable('');
        self.EditCustomerCpf = ko.observable('');
        self.EditBirthDate = ko.observable('');
        self.EditTagUID = ko.observable('');
        self.EditHaveABlankFields = ko.observable(false);

        self.NewCustomerSubmit = function (formElement) {
            debugger;
            if (self.NewCustomerFormIsValid()) {
                $.post('/Customer/AddNewCustomer/', $(formElement).serialize(), SubmittedWithSuccess, 'json');
            }
        }

        self.CpfIsChange = function () {
            $.ajax({
                type: 'POST',
                url: '/Customer/IsAlreadyExistsCustomer/',
                data: { 'cpf': self.CustomerCpf() },
                success: function (data) {
                    if (data) {
                        self.AlreadyExistsThisCustomer(true);
                    } else {
                        self.AlreadyExistsThisCustomer(false);
                    }
                }
            });
        }

        self.EditCustomer = function (customer) {
            self.EditCustomerId(customer.CustomerId);
            self.EditCustomerName(customer.CustomerName);
            self.EditCustomerCpf(customer.CustomerCpf);
            self.EditBirthDate(customer.FormattedBirthDate);
            self.EditTagUID(customer.TagUID);

            $('#edit-customer-modal').modal('show');
        }

        self.RemoveCustomer = function (customer) {
            $.ajax({
                type: 'POST',
                url: '/Customer/RemoveCustomer/',
                data: { 'cpf': customer.CustomerCpf },
                success: function (data) {
                    if (data) {
                        iziToast.success({
                            title: 'Sucesso!',
                            message: 'O cliente foi removido com sucesso!',
                            position: 'topRight'
                        });
                        GetAllCustomer();
                    }
                }
            });
        }
        
        self.EditCustomerSubmit = function (formElement) {
            if (self.EditCustomerFormIsValid()) {
                $.post('/Customer/EditCustomer/', $(formElement).serialize(), EditWithSuccess, 'json');
            }            
        }

        self.EditCustomerFormIsValid = function () {
            if (self.EditCustomerName() === '') {
                self.EditHaveABlankFields(true);
                return false;
            }

            if (self.EditCustomerCpf() === '') {
                self.EditHaveABlankFields(true);
                return false;
            }

            if (self.EditBirthDate() === '') {
                self.EditHaveABlankFields(true);
                return false;
            }

            if (self.EditTagUID === '') {
                self.EditHaveABlankFields(true);
                return false;
            }

            self.HaveABlankFields(false);
            return true;
        }

        function EditWithSuccess(data) {
            if (data.Success) {
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'As informações do cliente foram atualizadas!',
                    position: 'topRight'
                });
                GetAllCustomer();
                self.Error(false);
            } else
            {
                self.Error(true);
            }
        }

        function GetAllCustomer() {
            $.ajax({
                type: 'GET',
                url: '/Customer/GetAllCustomer/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));
                        self.Customers(response);
                        console.log(self.Customers());
                    }
                }
            });
        }

        self.NewCustomerFormIsValid = function () {
            if (self.CustomerName() === '') {
                self.HaveABlankFields(true);
                return false;
            }

            if (self.CustomerCpf() === '') {
                self.HaveABlankFields(true);
                return false;
            }

            if (self.BirthDate() === '') {
                self.HaveABlankFields(true);
                return false;
            }

            if (self.TagUID === '') {
                self.HaveABlankFields(true);
                return false;
            }

            if (self.AlreadyExistsThisCustomer() === true) {
                return false;
            }

            self.HaveABlankFields(false);
            return true;
        }

        function SubmittedWithSuccess(data) {
            if (data.Success) {
                iziToast.success({
                    title: 'Sucesso!',
                    message: 'O cliente foi adicionado com sucesso!',
                    position: 'topRight'
                });
                GetAllCustomer();
                self.Error(false);
            } else {
                self.Error(true);
            }
        }

        function SetMaskInput() {
            $('#CustomerCpf').mask('999.999.999.99');
            $('.date').mask('99-99-9999');
        }
        self.InitializeComponent = function () {
            GetAllCustomer();
            SetMaskInput();
        }();
    };

    var newCustomerModal = document.getElementById('new-customer-modal');
    ko.applyBindings(new newCustomerViewModel());
});