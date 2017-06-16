$(document).ready(function () {
    window.report = $('#table').DataTable({
        "ajax": {
            url: "/LockerReport/GetRentalByCustomerReport/",
            type: 'GET',
            cache: false,
            dataSrc: function (data) {
                try {
                    return data;
                } catch (e) {
                    return false;
                }
            }
        },
        "columns": [
            { "data": "LockerId" },
            { "data": "NumberOfPositionLocker" },
            { "data": "CustomerName" },
            { "data": "SectorName" },
            { "data": "SectorLocationName" },
            { "data": "FormattedInitialRentalDate" },
            { "data": "FormattedFinalRentalDate" }
        ],
        "language": {
            "lengthMenu": "Exibir _MENU_  Registros",
            "paginate": {
                "first": "Primeira",
                "last": "Última",
                "next": "Próximo",
                "previous": "Anterior"
            },
            "info": "Exibindo _START_ a _END_ de _TOTAL_ Registros",
            "infoEmpty": "Não houve resultados para sua busca",
            "emptyTable": "Não houve resultados para sua busca",
            "search": "Buscar:",
            "zeroRecords": "Não foi encontrado nenhum resultado",
            "infoFiltered": "(pesquisado em _MAX_ vendas dos últimos 7 dias)"
        }
    });

    $('#filter').click(function () {
        $.fn.dataTable.ext.search = [];
        $.fn.dataTable.ext.search.push(
            function (settings, data, dataIndex) {
                debugger;
                var dateRange = $('#dateRange').val();

                var dateSplit = dateRange.split(' ');

                var initialDateString = dateSplit[0];
                var initialDate = moment(initialDateString, "DD/MM/YYYY");

                var finalDateString = dateSplit[2];
                var finalDate = moment(finalDateString, "DD/MM/YYYY");

                var dateFormatted = data[5] || 0;
                var lineDate = moment(dateFormatted, "DD/MM/YYYY");

                if (lineDate >= initialDate && lineDate <= finalDate) {
                    return true;
                }

                return false;
            }
        );

        report.draw();
    });
});