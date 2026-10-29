using System.Text;
using TodoApp.Enums;
using TodoApp.Models;
using TodoApp.Services;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var manager = new TaskManager();

while (true)
{
    Console.WriteLine("\n===== TODO APP =====");
    Console.WriteLine("1. Thêm công việc");
    Console.WriteLine("2. Xem tất cả");
    Console.WriteLine("3. Sửa công việc");
    Console.WriteLine("4. Xóa công việc");
    Console.WriteLine("5. Đánh dấu hoàn thành");
    Console.WriteLine("6. Lọc công việc");
    Console.WriteLine("0. Thoát");
    Console.Write("Chọn: ");

    try
    {
        switch (Console.ReadLine())
        {
            case "1": AddTask(); break;
            case "2": PrintTasks(manager.GetAll()); break;
            case "3": EditTask(); break;
            case "4":
                Console.WriteLine(manager.Remove(ReadInt("Nhập Id: ")) ? "Đã xóa." : "Không tìm thấy.");
                break;
            case "5":
                Console.WriteLine(manager.MarkCompleted(ReadInt("Nhập Id: ")) ? "Đã hoàn thành." : "Không tìm thấy.");
                break;
            case "6": FilterTasks(); break;
            case "0": return;
            default: Console.WriteLine("Lựa chọn không hợp lệ."); break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Lỗi: {ex.Message}");
    }
}

void AddTask()
{
    Console.Write("Tiêu đề: ");
    var title = Console.ReadLine() ?? "";
    Console.Write("Mô tả: ");
    var desc = Console.ReadLine();
    var task = manager.Add(title, desc, ReadPriority(), ReadDate());
    Console.WriteLine($"Đã thêm công việc #{task.Id}.");
}

void EditTask()
{
    var id = ReadInt("Nhập Id cần sửa: ");
    Console.Write("Tiêu đề mới: ");
    var title = Console.ReadLine() ?? "";
    Console.Write("Mô tả mới: ");
    var desc = Console.ReadLine();
    var ok = manager.Update(id, title, desc, ReadPriority(), ReadDate());
    Console.WriteLine(ok ? "Đã cập nhật." : "Không tìm thấy.");
}

void FilterTasks()
{
    Console.Write("Trạng thái (1: chưa xong, 2: đã xong, Enter: bỏ qua): ");
    bool? done = Console.ReadLine() switch { "1" => false, "2" => true, _ => null };
    Console.Write("Ưu tiên (1: Thấp, 2: Vừa, 3: Cao, Enter: bỏ qua): ");
    Priority? pr = int.TryParse(Console.ReadLine(), out var p) && Enum.IsDefined(typeof(Priority), p)
        ? (Priority)p : null;
    PrintTasks(manager.Filter(done, pr));
}

void PrintTasks(IEnumerable<TaskItem> tasks)
{
    var list = tasks.ToList();
    if (list.Count == 0) { Console.WriteLine("Không có công việc nào."); return; }
    foreach (var t in list)
    {
        var status = t.IsCompleted ? "[x]" : "[ ]";
        var due = t.DueDate?.ToString("dd/MM/yyyy") ?? "không có";
        Console.WriteLine($"{status} #{t.Id} {t.Title} | {t.Priority} | Hạn: {due}");
    }
}

int ReadInt(string prompt)
{
    Console.Write(prompt);
    return int.TryParse(Console.ReadLine(), out var n) ? n : -1;
}

Priority ReadPriority()
{
    Console.Write("Ưu tiên (1: Thấp, 2: Vừa, 3: Cao): ");
    return int.TryParse(Console.ReadLine(), out var p) && Enum.IsDefined(typeof(Priority), p)
        ? (Priority)p : Priority.Medium;
}

DateTime? ReadDate()
{
    Console.Write("Hạn chót (dd/MM/yyyy, Enter để bỏ qua): ");
    return DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null,
        System.Globalization.DateTimeStyles.None, out var d) ? d : null;
}