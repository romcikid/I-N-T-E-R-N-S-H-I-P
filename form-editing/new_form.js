
$(document).ready(function(){

	$("#delete_me").click(function(){
		$("#my_fieldset").remove();
	});

	$("#plus_to_me").click(function(){
		$("#clone_me").clone(true).appendTo("#for_fields");
	});

	$("#your_select").change(function(){
		switch ($("#your_select :selected").html()) {

			case "Один из списка":
			case "Несколько из списка":
			case "Раскрывающийся список": $("#change_me").remove();
										$("#put_there").prepend($('<textarea>', {
											        id: "change_me",
											        class: "form-control",
											        placeholder: 'Questions' })); break;

			case "Текст - строка":
			case "Текст - абзац": $("#change_me").remove(); 
								$("#put_there").prepend($('<input>', {
											        type: 'text',
											        id: "change_me",
											        class: "form-control",
											        value: 'Placeholder'})); break;
	}});

});