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

/*// Get the modal
var modal = document.getElementById("bg-model");

// Get the button that opens the modal
var btn = document.getElementById("user-profile");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function() {
  modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
  modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}



document.getElementById('user-profile').addEventListener('click', function() {
	document.querySelector('.bg-modal').style.display ='flex';
});*/



$(document).ready(function(){
              
    $(".profile .icon_wrap").click(function(){
            $(this).parent().toggleClass("active");
            
    });

   
  });

  
  






