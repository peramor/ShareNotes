// Write your Javascript code.
$(document).ready(function () {
    $('label.tree-toggle').click(function () {
	    $(this).parent().children('ul.tree').toggle(200);
    });

     $('.lecture_note').mousemove(function(e){
        // положение элемента
        var pos = $(this).offset();
        var elem_left = pos.left;
        var elem_top = pos.top;
        // положение курсора внутри элемента
        var Xinner = e.pageX - elem_left;
        var Yinner = e.pageY - elem_top;
        this.X = Xinner;
        this.Y = Yinner;
        console.log("X: " + Xinner + " Y: " + Yinner); // вывод результата в консоль
      });

   
    $('label.tree-toggle').parent().children('ul.tree').toggle(200);
    
});