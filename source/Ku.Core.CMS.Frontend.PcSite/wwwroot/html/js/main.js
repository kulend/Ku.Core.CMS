$(document).ready(function(){
	var int=self.setInterval("qiPlay()",5000)
	var cgs=setInterval("cgPlay()",3000)
})

var qiNowPlay = 0;
var cgNowPlay = 0;
function qiPlay(){
	xp = qiNowPlay * 368 * -1;
	$("#qiPlayer").animate({top:xp+'px'});
	//alert(xp);
	qiNowPlay += 1;
	if (qiNowPlay==4){
		qiNowPlay = 0;
		}
	}

function cgPlay(){
	xps = cgNowPlay * 26 * -1;
	$("#cgBox").animate({top:xps+'px'});
	$("#gyBox").animate({top:xps+'px'});
	//alert(xps);
	cgNowPlay += 1;
	if (cgNowPlay==9){
		cgNowPlay = 0;
		}
	}