$(document).ready(function () {
    //window.report = $('#table').DataTable({
    //    "ajax": {
    //        url: "/LockerReport/GetUsingOfLockerReport/",
    //        type: 'GET',
    //        cache: false,
    //        dataSrc: function (data) {
    //            try {
    //                window.usingOfLockerReportData = data;
    //                //GenerateChart(data)
    //                return data;
    //            } catch (e) {
    //                return false;
    //            }
    //        }
    //    },
    //    "columns": [
    //        { "data": "FormattedDate" },
    //        { "data": "Hour" },
    //        { "data": "SectorLocationName" },
    //        { "data": "SectorName" },
    //        { "data": "TotalNumberOfLockers" },
    //        { "data": "TotalLockersOfUsing" },
    //        { "data": "FormattedPercentageOfUse" }
    //    ],
    //    "language": {
    //        "lengthMenu": "Exibir _MENU_ Registros",
    //        "paginate": {
    //            "first": "Primeira",
    //            "last": "Última",
    //            "next": "Próximo",
    //            "previous": "Anterior"
    //        },
    //        "info": "Exibindo _START_ a _END_ de _TOTAL_ Registros",
    //        "infoEmpty": "Não houve resultados para sua busca",
    //        "emptyTable": "Não houve resultados para sua busca",
    //        "search": "Buscar:",
    //        "zeroRecords": "Não foi encontrado nenhum resultado",
    //        "infoFiltered": "(pesquisado em _MAX_ registros)"
    //    }
    //});

    //function GetReportDataByDate() {
    //    var dateRange = $('#dateRange').val();

    //    var dateSplit = dateRange.split(' ');

    //    var initialDateString = dateSplit[0];
    //    var initialDate = moment(initialDateString, "DD/MM/YYYY");

    //    var finalDateString = dateSplit[2];
    //    var finalDate = moment(finalDateString, "DD/MM/YYYY");

    //    var response = [];
    //    for (var i = 0; i < usingOfLockerReportData.length; i++) {
    //        var data = usingOfLockerReportData[i];

    //        var dateFormatted = data.FormattedDate;
    //        var lineDate = moment(dateFormatted, "DD/MM/YYYY");

    //        if (lineDate >= initialDate && lineDate <= finalDate) {
    //            response.push(data);
    //        }
    //    }

    //    console.log(response);
    //    GenerateChart(response);
    //}

    function GetReportUsageSectorByDate() {
        debugger;
        var dateRange = $('#dateRange').val();

        var dateSplit = dateRange.split(' ');

        var initialDateString = dateSplit[0] + " " + "00:00:00";

        var finalDateString = dateSplit[2] + " " + "23:59:59";

        GenerateChart(initialDateString, finalDateString);
    }

    function GenerateChart(initialDateString, finalDateString) {
        $.ajax({
            type: 'POST',
            data: { "initialDate": initialDateString, "finalDate": finalDateString },
            url: '/LockerReport/GetUsageOfSectorReport/',
            success: function (data) {
                if (data) {
                    var response = $.parseJSON(JSON.stringify(data));
                    if (response.length > 0) {
                        $('#chartdiv').show();
                        $('#message-chart').hide();
                        CreateChart(response);
                    } else {
                        $('#message-chart').show();
                        $('#chartdiv').hide();
                    }
                    console.log(response);
                    
                }
            }
        });
    }

    function CreateChart(response) {
        var chart = AmCharts.makeChart("chartdiv", {
            "type": "pie",
            "language":"pt",
            "theme": "light",
            "legend": {
                "position": "right",
                "marginRight": 100,
                "autoMargins": false
            },
            "dataProvider": response,
            "valueField": "TotalOfActivitiesBySector",
            "titleField": "SectorName",
            "outlineAlpha": 0.4,
            "depth3D": 15,
            "balloonText": "[[title]]<br><span style='font-size:14px'><b>[[value]] Atividades</b> ([[percents]]%)</span>",
            "angle": 30,      
            "export": {
                "enabled": true
            }
        });
    }

     $('#filter').click(function () {       
        GetReportUsageSectorByDate()
    });

     var Initialize = function () {
         var initialDate = moment().format("DD/MM/YYYY") + " " + "00:00:00";
         var finalDate = moment().format("DD/MM/YYYY") + " " + "23:59:59";

         GenerateChart(initialDate, finalDate)
     }();
});