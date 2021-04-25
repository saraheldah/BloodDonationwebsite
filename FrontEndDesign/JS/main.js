jQuery(document).ready(function(){
    "use strict";
    $('#slider-carousel').caroufredsel({
        responsive:true,
        width: '100%',
        circular: true,
        scroll:{
            items:1,
            duration:500,
            pauseOnHover:true

                },
                auto:true,
                items:
                {
                    visible:{
                        min:1,
                        max:1
                    },
                    height:"variable"
                },
                pagination:{
                    container:".sliderpaher",
                    pageAnchorBuilder:false
                }
    }); 
    $(window).scroll(function(){
        var top = $(window).scrollTop();
        if(top>=60){
            $("header").addClass('secondary');
        }
        else if( $("header").hasClass('secondary')){
            $("header").removeClass('secondary');
        }

    });

});
const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});