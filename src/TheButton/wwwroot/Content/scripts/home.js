/// <reference path="../../../../../typings/d3/d3.d.ts"/>
/// <reference path="../../../../../typings/jquery/jquery.d.ts"/>
var buttonData;
var svg;

$(function () {
	d3.select('button').on('click', loadButtonClick);
});

function loadButtonClick() {
	d3.select('#welcomeDiv').remove();
	var loadingBar = d3.select('div').append('div')
		.attr('class', 'progress')
	loadingBar.append('div')
			.attr('class', 'progress-bar progress-bar-striped active')
			.style('width', '100%');
	d3.csv('/api/stats/all', function (d) {
	    var pressTime = moment(d['press time']);
	    var seconds = null;
	    if (d['seconds'] != 'non presser') {
	        seconds = parseInt(d['seconds'].slice(0, -1));
	    }
	    var css = d['css'];
        var outagePress = (d['outage press'] === 'true')
        return {
            press_time: pressTime,
            seconds: seconds,
            css: css,
            outage_press: outagePress
        };
	}, function (error, rows) {
	    buttonData = rows;
	    loadingBar.remove();
	    console.log('done');
	});
	//$.getJSON('/api/stats/all',
	//	function (data) {
	//		buttonData = data;
	//		loadingBar.remove();
	//		draw();
	//	});
}

function draw() {
	svg = d3.select('div').append('svg');
	svg.attr('width', '1920').attr('height', '1080');
	svg.selectAll('circle').data(buttonData).enter()
		.append('circle')
		.attr('cx', function () {
			return randomTo(1920);
		})
		.attr('cy', function () {
			return randomTo(1080);
		})
		.attr('r', function (d) {
			if (d.seconds != null) {
				return d.seconds + 1;
			}
		});
}

function randomTo(num) {
	return Math.floor(Math.random() * num);
}