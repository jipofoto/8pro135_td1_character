extends CharacterBody2D

@export var move_speed : float = 200

func _physics_process(_delta: float) -> void:
	var input_direction = Vector2(
		Input.get_action_strength("move_right") - Input.get_action_strength("move_left"),
		Input.get_action_strength("move_down") - Input.get_action_strength("move_up")
	)

	#update velocity
	velocity = input_direction * move_speed
	
	if velocity.length() > 0:
		velocity = velocity.normalized() * move_speed
		$AnimatedSprite2D.play()
	else:
		$AnimatedSprite2D.stop()
	
	if velocity.x < 0:
		$AnimatedSprite2D.animation = "move_left"
	if velocity.x > 0:
		$AnimatedSprite2D.animation = "move_right"
	if velocity.y < 0:
		$AnimatedSprite2D.animation = "move_up"
	if velocity.y > 0:
		$AnimatedSprite2D.animation = "move_down"


	move_and_slide()
