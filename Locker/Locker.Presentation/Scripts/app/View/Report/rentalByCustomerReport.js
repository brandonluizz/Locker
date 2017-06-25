$(document).ready(function () {
    window.report = $('#table').DataTable({
        "ajax": {
            url: "/LockerReport/GetRentalByCustomerReport/",
            type: 'GET',
            cache: false,
            dataSrc: function (data) {
                try {
                    if (data.length > 0) {
                        GenerateChart();
                    }
                    $('#filter').click();

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
        GenerateChart();
        report.draw();
    });

    function GenerateChart() {
        var dateRange = $('#dateRange').val();

        var dateSplit = dateRange.split(' ');

        var initialDateString = dateSplit[0] + " " + "00:00:00";

        var finalDateString = dateSplit[2] + " " + "23:59:59";
        debugger;

        $.ajax({
            type: 'POST',
            data: { "initialDate": initialDateString, "finalDate": finalDateString },
            url: '/LockerReport/GetUsageOfClientReport/',
            success: function (data) {
                if (data) {
                    var response = $.parseJSON(JSON.stringify(data));
                    if (response.length > 0) {
                        CreateChart(response);
                    } else {
                    }
                    console.log(response);

                }
            }
        });
    }

    function CreateChart(response) {
        debugger;

        var chart = AmCharts.makeChart("chartdiv", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": response,   
            "valueAxes": [{
                "position": "left",
                "title": "Total de Locação"
            }],
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]] Locações</b>",
                "fillAlphas": 1,
                "lineAlpha": 0.1,
                "type": "column",
                "valueField": "TotalOfActivities"
            }],
            "depth3D": 20,
            "angle": 30,
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            }, 
            "categoryField": "CustomerName",
            "categoryAxis": {
                "gridPosition": "start",
                "labelRotation": 90
            },
            "export": {
                "enabled": false
            }

        });
    }
});