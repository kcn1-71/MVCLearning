function deleteModal() {
	var deleteButtons = $('button[data-target="#modal-delete"]'),
		modalTextPaste = $('#object-to-delete');
	$(document).on('click', 'button[data-target="#modal-delete"]', function(){
		var button = $(this),
			type = button.attr('data-delete-target-type'),
			target = $('button[href='+button.attr('data-delete-target')+']'),
			name = $(target).text(),
			textToPaste = type +' "'+ name + '"';
			modalTextPaste.html(textToPaste);
	});
}

function editName() {
	
}