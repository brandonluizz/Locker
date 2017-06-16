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

    function GenerateChart(response) {
        var chart = AmCharts.makeChart("chartdiv", {
            "type": "serial",
            "theme": "light",
            "marginRight": 70,
            "legend": {
                "equalWidths": false,
                "useGraphSettings": true,
                "valueAlign": "left",
                "valueWidth": 120
            },
            "dataProvider": response,
            "valueAxes": [{
                "id": "hourAxis",
                "Hour": "hh",
                "hourUnits": {
                    "hh": "h ",
                    "mm": "min"
                },
                "axisAlpha": 0,
                "gridAlpha": 0,
                "inside": true,
                "position": "right",
                "title": "Hora"
            }],
            "graphs": [{
                "alphaField": "alpha",
                "balloonText": "[[value]]% de Uso",
                "dashLengthField": "dashLength",
                "fillAlphas": 0.7,
                "legendPeriodValueText": "Porcentagem: ",
                "legendValueText": "[[value]]%",
                "title": "Porcentagem de Uso",
                "type": "column",
                "valueField": "PercentageOfUse",
                "valueAxis": "PercentageOfUseAxis"
            }, {
                "balloonText": "Total de Armários:[[value]]",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "useLineColorForBulletBorder": true,
                "bulletColor": "#FFFFFF",
                "dashLengthField": "dashLength",
                "labelPosition": "right",
                "legendValueText": "[[value]]",
                "title": "Total de Armários",
                "fillAlphas": 0,
                "valueField": "TotalNumberOfLockers"
            }
                , {
                "bullet": "square",
                "bulletBorderAlpha": 1,
                "bulletBorderThickness": 1,
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": "Hora",
                "fillAlphas": 0,
                "valueField": "Hour",
                "valueAxis": "hourAxis"
            }],
            "chartCursor": {
                "categoryBalloonDateFormat": "DD",
                "cursorAlpha": 0.1,
                "cursorColor": "#000000",
                "fullWidth": true,
                "valueBalloonsEnabled": false,
                "zoomable": false
            },
            "dataDateFormat": "DD-MM-YYYY",
            "categoryField": "FormattedDate",
            "categoryAxis": {
                "dateFormats": [{
                    "period": "DD",
                    "format": "DD"
                }, {
                    "period": "WW",
                    "format": "MMM DD"
                }, {
                    "period": "MM",
                    "format": "MMM"
                }, {
                    "period": "YYYY",
                    "format": "YYYY"
                }],
                "parseDates": true,
                "autoGridCount": false,
                "axisColor": "#555555",
                "gridAlpha": 0.1,
                "gridColor": "#FFFFFF",
                "gridCount": 50
            },
            "export": {
                "enabled": true
            }
        });
    }


    $('#filter').click(function () {
        var teste = [];
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
                    debugger;
                    teste.push(report.rows().data()[dataIndex]);
                    return true;
                }
                return false;
            }
        );
       // GenerateChart(teste)

        report.draw();
    });

    ko.applyBindings(new viewModel());
});