let draw = false;

init();
/**
 * FUNCTIONS
 */

function init() {
    
    // initialize DataTables
    const table = $('#dt-table').DataTable({
        scrollY: "300px",
        scrollX: true,
        scrollCollapse: true,
        paging: false,
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        fixedColumns: {
            leftColumns: 1,
        }
    });
    // get table data
    const tableData = getTableData(table);
    const playerDetails = getPlayerName(table);
    // create Highcharts
    createHighcharts(tableData, playerDetails);
    // table events
    setTableEvents(table);
}

function getPlayerName(table) {
    var playerName;
    table.rows({ search: "applied" }).every(function () {
        const data = this.data();
        playerName = data[0];
    })
    return playerName;
}

function getTableData(table) {
    const dataArray = [],
        countryArray = [],
        populationArray = [],
        densityArray = [];

    // loop table rows
    table.rows({ search: "applied" }).every(function () {
        const data = this.data();
        countryArray.push(data[1]);
        populationArray.push(parseInt(data[2].replace(/\,/g, "")));
        densityArray.push(parseInt(data[3].replace(/\,/g, "")));
    });

    // store all data in dataArray
    dataArray.push(countryArray, populationArray, densityArray);

    return dataArray;
}

function createHighcharts(data, playerDetails) {
    Highcharts.setOptions({
        lang: {
            thousandsSep: ","
        }
    });
    //var playerName = getPlayerName();

    Highcharts.chart("chart", {
        title: {
            text: playerDetails
        },
        subtitle: {
            text: "Data from Dream11"
        },
        xAxis: [
            {
                categories: data[0],
                labels: {
                    rotation: -45
                }
            }
        ],
        yAxis: [
            {
                // first yaxis
                title: {
                    text: "Selection Rate"
                }
            },
            {
                // secondary yaxis
                title: {
                    text: "Points"
                },
                min: 0,
                max: 100,
                opposite: true
            }
        ],
        series: [
            {
                name: "Selection Rate",
                color: "#0071A7",
                type: "column",
                data: data[1],
                tooltip: {
                    valueSuffix: "%"
                }
            },
            {
                name: "Points",
                color: "#FF404E",
                type: "spline",
                data: data[2],
                yAxis: 1
            }
        ],
        tooltip: {
            shared: true
        },
        legend: {
            backgroundColor: "#ececec",
            shadow: true
        },
        credits: {
            enabled: false
        },
        noData: {
            style: {
                fontSize: "16px"
            }
        }
    });
}

function setTableEvents(table) {
    // listen for page clicks
    table.on("page", () => {
        draw = true;
    });

    // listen for updates and adjust the chart accordingly
    table.on("draw", () => {
        if (draw) {
            draw = false;
        } else {
            const tableData = getTableData(table);
            const playerDetails = getPlayerName(table);
            createHighcharts(tableData, playerDetails);
        }
    });
}
