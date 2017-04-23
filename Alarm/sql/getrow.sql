SELECT
	tasks.id,
	tasks.state,
	tasks.title,
	tasks.content,
	tasks.type,
	tasks.time,
	tasks.date,
	tasks.weekdays,
	tasks.audio,
	tasks.volume,
	tasks.fontsize
FROM
	tasks
WHERE
	tasks.id = @id;