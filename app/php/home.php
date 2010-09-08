<?php
/**
 * Task JSON Format:
 * Id
 * Name
 * Priority
 * StartDate - date
 * DueDate - date
 * HoursSpent - decimal that is number of hours
 * HoursRemainig - decimal that is number of hours
 */
class Task {
	var $Id;
	var $Name;
	var $Priority;
	var $StartDate;
	var $DueDate;
	var $HoursSpent;
	var $HoursRemaining;
}

session_start();
if ( !isset($_SESSION['tasks']) ) {
	$_SESSION['tasks'] = array();
}
if ( !isset($_SESSION['priorities']) ) {
	$_SESSION['priorities'] = array('low', 'medium', 'high', 'critical');
}

header('Content-type: text/plain');

$parts  = explode('/', preg_replace('/^.*home\.php\//', '', $_SERVER['REQUEST_URI']) );
$action = array_shift($parts);
$id     = array_shift($parts);

switch ($action) {
	case 'reset':
		unset($_SESSION['tasks']);
		unset($_SESSION['priorities']);
		unset($_SESSION);
		break;
	case 'priorities':
		echo json_encode($_SESSION['priorities']);
		break;
	case 'task':
		if ( $id == 'create' ) {
			$task = new Task();
			$task->Id = count($_SESSION['tasks']);
			foreach (array('Name', 'Priority', 'StartDate', 'DueDate', 'HoursSpent', 'HoursRemaining') AS $k ) {
				$task->{$k} = $_REQUEST[$k];
			}
			$_SESSION['tasks'][$task->Id] = $task;
			echo 'true';
		} else {
			if ( isset($_SESSION['tasks'][$id]) ) {
				echo json_encode($_SESSION['tasks'][$id]);
			} else {
				echo 'false';
			}
		}
		break;

	case 'index':
	default:
		echo json_encode($_SESSION['tasks']);
}
