(function($){
	$(function(){
	  
	    //var shiftWindow = function () { scrollBy(0, -210) };
	    //window.addEventListener("hashchange", shiftWindow);
	    //function load() { if (window.location.hash) shiftWindow(); }

	    var wrapper_top = $("#MenuPart").offset().top;
	

	  // Smooth Scroll Links
	  $("#MenuPart a").click(function (e) {
	  	e.preventDefault();

	  	var hash = this.hash.substr(1);
	  	console.log(hash);
	  	$('html, body').animate({
        scrollTop: $("#"+ hash).offset().top - 210
    }, 500);

	  });

	}); // end of document ready
})(jQuery); // end of jQuery name space




(function ($) {
    $(function () {

        //var shiftWindow = function () { scrollBy(0, -210) };
        //window.addEventListener("hashchange", shiftWindow);
        //function load() { if (window.location.hash) shiftWindow(); }

        var wrapper_top = $("#FooterMenu").offset().top;


        // Smooth Scroll Links
        $("#FooterMenu a").click(function (e) {
            e.preventDefault();

            var hash = this.hash.substr(1);
            console.log(hash);
            $('html, body').animate({
                scrollTop: $("#" + hash).offset().top - 210
            }, 500);

        });

    }); // end of document ready
})(jQuery); // end of jQuery name space




