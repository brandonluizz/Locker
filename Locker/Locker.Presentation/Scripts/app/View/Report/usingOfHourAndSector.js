$(document).ready(function () {   
    $('#filter').click(function () {
        var dateRange = $('#dateRange').val();

        var dateSplit = dateRange.split(' ');

        var initialDateString = dateSplit[0] + " " + "00:00:00";

        var finalDateString = dateSplit[2] + " " + "23:59:59";

        GenerateChart(initialDateString, finalDateString);
    });

    function GenerateChart(initialDateString, finalDateString) {
        $.ajax({
            type: 'POST',
            data: { "initialDate": initialDateString, "finalDate": finalDateString },
            url: '/LockerReport/GetUsageOfHourAndSectorReport/',
            success: function (data) {
                if (data) {
                    var response = $.parseJSON(JSON.stringify(data));
                    if (response.length > 0) {
                        $('#chartdiv').show();
                        $('#not-found-message').hide();
                        CreateChart(response);
                    } else {                        
                        $('#chartdiv').hide();
                        $('#not-found-message').show();
                    }
                    console.log(response);

                }
            }
        });
    }

    function CreateChart(response) {
        var chart = AmCharts.makeChart("chartdiv", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": response,
            "legend": {
                "useGraphSettings": true,
                "markerSize": 12,
                "valueWidth": 0,
                "verticalGap": 0
            },
            "valueAxes": [{
                "position": "left",
                "title": "Total de Locação"
            }],
            "graphs": [{
                "balloonText": "Total de Locação em [[category]]: <b>[[value]]</b>",
                "fillAlphas": 1,
                "lineAlpha": 0.1,
                "type": "column",
                "valueField": "TotalOfActivities",
                "title": "Locações",
            }, {
                "balloonText": "<span style='font-size:13px;'>[[title]]:<b>[[value]]Hr</b></span>",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "bulletColor": "#FFFFFF",
                "useLineColorForBulletBorder": true,
                "fillAlphas": 0,
                "lineThickness": 2,
                "lineAlpha": 1,
                "bulletSize": 7,
                "title": "Horário de Locação",
                "valueField": "Hour",
            }],
            "depth3D": 20,
            "angle": 30,
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "SectorName",
            "categoryAxis": {
                "gridPosition": "start",
                "labelRotation": 90
            },
            "export": {
                "enabled": false
            }

        });  
    }

    var Initialize = function () {
        var initialDate = moment().format("DD/MM/YYYY") + " " + "00:00:00";
        var finalDate = moment().format("DD/MM/YYYY") + " " + "23:59:59";

        GenerateChart(initialDate, finalDate)
    }();
});