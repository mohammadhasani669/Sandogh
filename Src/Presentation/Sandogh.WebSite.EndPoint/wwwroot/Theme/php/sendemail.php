<?php

/*
	The Send Mail php Script for Contact Form
	Server-side data validation is also added for good data validation.
*/

$name = $_POST['username'];
$email = $_POST['email'];
$subject = $_POST['subject'];
$message = $_POST['message'];

if( $name == '' ){
	die('لطفا نام خود را وارد نمایید!');
}
else if( $email == '' ){
	die('لطفا ایمیل خود را وارد نمایید!');
}
else if(filter_var($email, FILTER_VALIDATE_EMAIL) == false){
	die('لطفا یک ایمیل معتبر وارد نمایید!');
}
else if( $subject == '' ){
	die('لطفا موضوع را وارد نمایید!');
}
else if( $message == '' ){
	die('لطفا پیام خود را وارد نمایید!');
}
else{
	
	//Place your Email Here
	$recipient = "info@example.com";
	
	$formcontent="نام: $name\nایمیل: $email\nموضوع: $subject\n\nپیام:\n$message";
	
	$mailheader = "From:$email\r\n";
	
	if( mail($recipient, 'پیام جدید در سایت', $formcontent, $mailheader) ){
		die('پیام شما با موفقیت ارسال شد!');
	}
	else{
		die('خطا در ارسال پیام!');
	}
}

?>