$(document).ready(function () {
    var viewModel = function () {
        var self = this;

        function GetReport() {
            $.ajax({
                type: 'GET',
                url: '/LockerReport/GetUsingOfLockerReport/',
                success: function (data) {
                    if (data) {
                        var response = $.parseJSON(JSON.stringify(data));
                        //GenerateChart(response);
                        console.log(data);
                    }
                }
            });
        }

        
        self.InitializeComponent = function () {
            GetReport();
        }();
    }



    window.report = $('#table').DataTable({
        "ajax": {
            url: "/LockerReport/GetUsingOfLockerReport/",
            type: 'GET',
            cache: false,
            dataSrc: function (data) {
                try {
                    window.usingOfLockerReportData = data;
                    GenerateChart(data) 
                    return data;
                } catch (e) {
                    return false;
                }
            }
        },
        "columns": [
            { "data": "FormattedDate" },
            { "data": "Hour" },
            { "data": "SectorLocationName" },
            { "data": "SectorName" },
            { "data": "TotalNumberOfLockers" },
            { "data": "TotalLockersOfUsing" },
            { "data": "FormattedPercentageOfUse" }
        ],
        "language": {
            "lengthMenu": "Exibir _MENU_ Registros",
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
            "infoFiltered": "(pesquisado em _MAX_ registros)"
        }
    });

    function GetReportDataByDate() {
        debugger;
        var dateRange = $('#dateRange').val();

        var dateSplit = dateRange.split(' ');

        var initialDateString = dateSplit[0];
        var initialDate = moment(initialDateString, "DD/MM/YYYY");

        var finalDateString = dateSplit[2];
        var finalDate = moment(finalDateString, "DD/MM/YYYY");

        var response = [];
        for (var i = 0; i < usingOfLockerReportData.length; i++) {
            var data = usingOfLockerReportData[i];

            var dateFormatted = data.FormattedDate;
            var lineDate = moment(dateFormatted, "DD/MM/YYYY");

            if (lineDate >= initialDate && lineDate <= finalDate) {
                response.push(data);
            }
        }

        console.log(response);
        GenerateChart(response);
    }

    function GenerateChart(response) {
        var chart = AmCharts.makeChart("chartdiv", {
            "type": "serial",
            "theme": "light",
            "handDrawn": true,
            "handDrawScatter": 3,
            "marginRight": 70,
            "legend": {
                "useGraphSettings": true,
                "markerSize": 12,
                "valueWidth": 0,
                "verticalGap": 0
            },
            "valueAxes": [{
                "axisAlpha": 0,
                "position": "left",
                "title": "Hora"
            }],
            "dataProvider": response,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": true,
                "position": "top",
                "axisAlpha": 0
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "<span style='font-size:13px;'>[[title]] dos armários:<b>[[value]]%</b></span>",
                "title": "Porcentagem de uso",
                "type": "column",
                "fillAlphas": 0.8,

                "valueField": "PercentageOfUse"
            }, {
                "balloonText": "<span style='font-size:13px;'>[[title]] na seção:<b>[[value]]</b></span>",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "bulletColor": "#FFFFFF",
                "useLineColorForBulletBorder": true,
                "fillAlphas": 0,
                "lineThickness": 2,
                "lineAlpha": 1,
                "bulletSize": 7,
                "title": "Quantidade de Armários",
                "valueField": "TotalNumberOfLockers"
            }],
            "rotate": true,
            "categoryField": "Hour",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
                "enabled": true
            }

        });
    }


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

                var dateFormatted = data[0] || 0;
                var lineDate = moment(dateFormatted, "DD/MM/YYYY");

                if (lineDate >= initialDate && lineDate <= finalDate) {
                    return true;
                }
                return false;
            }
        );
       GetReportDataByDate()

        report.draw();
    });

    ko.applyBindings(new viewModel());
});