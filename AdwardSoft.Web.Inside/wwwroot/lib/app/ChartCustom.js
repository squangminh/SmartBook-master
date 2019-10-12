function CircleChart(id, data, value, color, title, radius, tooltip) {
    var circle = document.getElementById(id);
    var Chart = echarts.init(circle)
    Chart.setOption({
        animationDuration: 500,
        color: color,

        title: {
            text: title,
            x: 'center'
        },
        legend: {
            orient: 'horizontal',
            y: 'bottom',
            data: data
        },
        tooltip: tooltip,
        series: [{
            type: 'pie',
            radius: radius,
            center: ['50%', '50%'],
            hoverOffset: 0,
            avoidLabelOverlap: false,
            label: {
                show: false,
                position: 'top',
                formatter: '{b}: {c}'
            },
            labelLine: {
                normal: {
                    show: false
                }
            },
            data: value
        }]
    })


}

function LineChart(id, data, xdata, value, color, title) {
    var line = document.getElementById(id);
    var Chart = echarts.init(line);
    var arrData = [];
    for (let i = 0; i < value.length; i++) {
        arrData.push(
            {
                type: 'line',
                smooth: true,
                data: value[i]
            })
    }

    Chart.setOption({
        animationDuration: 500,
        color: color,
        title: {
            text: title,
            x: 'center'
        },
        legend: {
            orient: 'horizontal',
            y: 'bottom',
            data: data
        },
        grid: {
            top: 70,
            bottom: 50
        },
        xAxis: {
            type: 'category',
            axisTick: {
                alignWithLabel: true
            },
            data: xdata
        },
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: arrData
    })
}