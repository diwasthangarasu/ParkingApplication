using System.Globalization;
using System.Text;
using System.Threading.Channels;
using ParkingApplication.Enums;
using ParkingApplication.Model;

namespace ParkingApplication.Repository;

public class CSVWriter
{
    private readonly string _filePath;

    private readonly Channel<Ticket> _channel;

    private const int BatchSize = 10;

    public CSVWriter(string filePath)
    {
        _filePath = filePath;

        _channel = Channel.CreateUnbounded<Ticket>();

        _ = ProcessTicketsAsync();
    }

    public async Task QueueTicket(Ticket ticket)
    {
        await _channel.Writer.WriteAsync(ticket);
    }

    private async Task ProcessTicketsAsync()
    {
        List<Ticket> batch = new(BatchSize);

        await foreach (Ticket ticket in _channel.Reader.ReadAllAsync())
        {
            batch.Add(ticket);

            if (batch.Count >= BatchSize)
            {
                await WriteBatchAsync(batch);

                batch.Clear();
            }
        }

        // Write remaining tickets when channel is completed
        if (batch.Count > 0)
        {
            await WriteBatchAsync(batch);
        }
    }

    public async Task<List<Ticket>> LoadTickets()
    {
        List<Ticket> tickets = new();

        if (!File.Exists(_filePath))
        {
            return tickets;
        }

        using StreamReader reader = new(_filePath);

        // Skip header
        await reader.ReadLineAsync();

        string? line;

        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] values = ParseCsvLine(line);

            Ticket ticket = new(
                Guid.Parse(values[0]),
                values[1],
                Enum.Parse<VehicleTypes>(values[2]),
                DateTime.Parse(
                    values[3],
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind),
                int.Parse(values[4]),
                int.Parse(values[5])
            );

            tickets.Add(ticket);
        }

        return tickets;
    }


    private async Task WriteBatchAsync(List<Ticket> tickets)
    {
        bool fileExists = File.Exists(_filePath);

        await using StreamWriter writer = new(
            _filePath,
            append: true,
            encoding: Encoding.UTF8);

        if (!fileExists)
        {
            await writer.WriteLineAsync(
                "TicketId,LicensePlate,VehicleType,EntryTime,Level,SlotNumber");
        }

        foreach (Ticket ticket in tickets)
        {
            string csvRow =
                $"{ticket.TicketId}," +
                $"{EscapeCsv(ticket.LicensePlate)}," +
                $"{ticket.VehicleType}," +
                $"{ticket.EntryTime:O}," +
                $"{ticket.Level}," +
                $"{ticket.SlotNumber}";

            await writer.WriteLineAsync(csvRow);
        }
    }

    public void Complete()
    {
        _channel.Writer.TryComplete();
    }

    private static string EscapeCsv(string value)
    {
        if (value.Contains(',') ||
            value.Contains('"') ||
            value.Contains('\n'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }

    private static string[] ParseCsvLine(string line)
    {
        List<string> values = new();
        StringBuilder current = new();

        bool insideQuotes = false;

        foreach (char character in line)
        {
            if (character == '"')
            {
                insideQuotes = !insideQuotes;
            }
            else if (character == ',' && !insideQuotes)
            {
                values.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(character);
            }
        }

        values.Add(current.ToString());

        return values.ToArray();
    }
}