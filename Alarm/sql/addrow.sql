INSERT OR REPLACE INTO tasks 
(
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
)
VALUES
(@id,@state,@title,content,@type,@time,@date,@weekdays,@audio,@volume,@fontsize);