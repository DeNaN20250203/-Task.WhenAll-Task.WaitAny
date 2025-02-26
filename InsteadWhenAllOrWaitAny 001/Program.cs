namespace InsteadWhenAllOrWaitAny_001
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
			// Создаем первую задачу с задержкой 2000 миллисекунд
			var task1 = DelayMs(2000);

			// Создаем вторую задачу с задержкой 5000 миллисекунд
			var task2 = DelayMs(5000);

			/// <summary>
			/// Список задач, которые будут обработаны.
			/// </summary>
			List<Task> taskList = new() { task1, task2 };

			/// <summary>
			/// Итерируемся по каждой завершенной задаче из списка.
			/// </summary>
			/// <remarks>
			/// Используется Task.WhenEach для ожидания завершения всех задач и обработки их результатов по порядку.
			/// </remarks>
			await foreach (var task in Task.WhenEach(taskList))
			{
				if (task.IsFaulted)
				{
					// Если задача завершилась с ошибкой, выводим информацию об исключении
					Console.WriteLine($"Task {task.Id} failed: {task.Exception}");
				}
				else
				{
					// Если задача успешно завершилась, выводим сообщение о завершении
					Console.WriteLine($"Task {task.Id} finished. Processing…");
				}
			}

			Console.ReadKey();
        }

		/// <summary>
		/// Метод, который выполняет задержку на указанное количество миллисекунд.
		/// </summary>
		/// <param name="ms">Количество миллисекунд для задержки.</param>
		/// <remarks>
		/// После завершения задержки выводится сообщение о том, сколько времени было задержано.
		/// </remarks>
		public static async Task DelayMs(int ms)
		{
			await Task.Delay(ms);
			Console.WriteLine($"Delayed for {ms} milliseconds…");
		}
	}
}
