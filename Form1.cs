using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PullSDK_core;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Timers;
using Microsoft.VisualBasic.ApplicationServices;

namespace Controles_Auxiliares
{
    public partial class Form1 : Form
    {
        AccessPanel ACDevice = new AccessPanel();
        private CancellationTokenSource loggingCancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Update status
            lblConnected.Visible = false;
            lblDisconnected.Visible = true;
            // Disable buttons
            btnDisconnect.Enabled = false;
            btnCreate.Enabled = false;
            btnDelete.Enabled = false;
            btnFinger.Enabled = false;
            btnDoor.Enabled = false;
            btnStartLogging.Enabled = false;
            btnMemberShip.Enabled = false;                        
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Connect to the access device
            if (!ACDevice.Connect("10.0.0.240", 4370, 0, 5000))
            {
                MessageBox.Show("No se pudo conectar...", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Dispositivo Conectado.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Update status
                lblDisconnected.Visible = false;
                lblConnected.Visible = true;
                // disable buttons
                btnConnect.Enabled = false;
                // Enable buttons
                btnDisconnect.Enabled = true;
                btnCreate.Enabled = true;
                btnDelete.Enabled = true;
                btnFinger.Enabled = true;
                btnDoor.Enabled = true;
                btnStartLogging.Enabled = true;
                btnMemberShip.Enabled = true;
                // write timezone
                int[] defaultTZ = new int[] {
                    2359, 0, 0, 
                    2359, 0, 0,
                    2359, 0, 0,
                    2359, 0, 0,
                    2359, 0, 0,
                    2359, 0, 0,
                    2359, 0, 0
                };
                if (!ACDevice.WriteTimezone(0, defaultTZ))
                {
                    return; // didnt work
                }                
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (ACDevice.IsConnected())
            {
                ACDevice.Disconnect();
                // Update status
                lblConnected.Visible = false;
                lblDisconnected.Visible = true;
                // Disable buttons
                btnDisconnect.Enabled = false;
                btnConnect.Enabled = true;
                btnCreate.Enabled = false;
                btnDelete.Enabled = false;
                btnFinger.Enabled = false;
                btnStartLogging.Enabled = false;
                btnMemberShip.Enabled = false;
                MessageBox.Show("Dispositivo desconectado.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("El dispositivo ya estaba desconectado..", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CreateUser()
        {
            string apiUrl = "https://www.naturalfitnessclubpos.com/api/members";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var members = JsonConvert.DeserializeObject<List<MemberData>>(responseBody);

                    if (members.Count > 0)
                    {
                        MemberData firstMember = members[0];
                        int id = 0;
                        if (firstMember.oldId == null)
                        {
                            id = firstMember.Id;
                        }
                        else
                        {
                            id = Convert.ToInt32(firstMember.oldId);
                        }
                        string idString = id.ToString();

                        Console.WriteLine("Connected");

                        // Get today's date
                        DateTime today = DateTime.Today;
                        // Calculate the end date as 31 days from today
                        DateTime endDate = today.AddDays(31);

                        // Format both dates as yyyymmdd
                        string startDateFormatted = today.ToString("yyyyMMdd");
                        string endDateFormatted = endDate.ToString("yyyyMMdd");

                        // Create a new User object with the required information
                        PullSDK_core.User newUser = new PullSDK_core.User(
                            pin: idString,
                            name: $"{firstMember.Nombre} {firstMember.Apellido}",
                            card: firstMember.NumeroTarjeta,
                            password: "",
                            startTime: startDateFormatted,
                            endTime: endDateFormatted
                            );                        

                        // Perform the user addition operation
                        if (!ACDevice.WriteUser(newUser))
                        {
                            MessageBox.Show("No se pudo sincronizar el dispositivo", "Estado", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Dispositivo sincronizado.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            StopLogging();
            if (!ACDevice.IsConnected())
            {
                ACDevice.Connect("10.0.0.240", 4370, 0, 5000);
            }
            else
            {
                await CreateUser();                
                StartLogging();
            }
        }

        private async Task DeleteUser()
        {
            string apiUrl = "https://www.naturalfitnessclubpos.com/api/deleted-members";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var members = JsonConvert.DeserializeObject<List<MemberData>>(responseBody);

                    if (members.Count > 0)
                    {
                        MemberData firstMember = members[0];
                        int id = 0;
                        if (firstMember.oldId == null)
                        {
                            id = firstMember.Id;
                        }
                        else
                        {
                            id = Convert.ToInt32(firstMember.oldId);
                        }
                        string idString = id.ToString();

                        if (!ACDevice.DeleteUser(idString))
                        {
                            MessageBox.Show("No se pudo sincronizar el dispositivo", "Estado", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Dispositivo sincronizado.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            StopLogging();
            if (!ACDevice.IsConnected())
            {
                ACDevice.Connect("10.0.0.240", 4370, 0, 5000);
            }

            else
            {
                await DeleteUser();
                StartLogging();
            }
        }

        private void FingerUser(string idString)
        {
            List<PullSDK_core.User> users = ACDevice.ReadUsers();
            if (users == null)
            {
                Console.WriteLine("couldt read");
                return;
            }
            foreach (PullSDK_core.User user in users)
            {
                if (user.Pin == idString)
                {
                    user.SetDoorsByFlag(1); // give access on door 1
                                            // Get today's date
                    DateTime today = DateTime.Today;
                    // Calculate the end date as 31 days from today
                    DateTime endDate = today.AddDays(31);

                    // Format both dates as yyyymmdd
                    string startDateFormatted = today.ToString("yyyyMMdd");
                    string endDateFormatted = endDate.ToString("yyyyMMdd");

                    user.StartTime = startDateFormatted;
                    // Set EndTime to the reformatted expiration date
                    user.EndTime = endDateFormatted;

                }
            }

            // Initialize the fingerprint reader library
            if (!FingerReader.Init())
            {
                Console.WriteLine("Problemas del .dll");
                return;
            }

            // Connect to the fingerprint reader device
            var reader = FingerReader.GetDevice();
            if (reader == null)
            {
                MessageBox.Show("Verifique el cable USB del lector.", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (reader.ReadParameters())
            {
                var db = new FingerprintDb();
                if (db.Init())
                {
                    int steps = 0;
                    MessageBox.Show("Después de este mensaje, presione su dedo cuantas veces sea necesario.", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (steps < FingerprintDb.Steps)
                    {
                        var data = reader.AcquireFingerprint();
                        if (data == null)
                        {
                            Thread.Sleep(100); // Wait                             
                            continue;
                        }
                        if (!db.Add(data[0]))
                        {
                            MessageBox.Show("Error, Presione su dedo otra vez.", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }
                        MessageBox.Show("Correcto... Procesando el paso actual...", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MemoryStream ms = new MemoryStream();
                        var bmpImage = ms.ToArray();

                        // move to next step
                        steps++;
                        Thread.Sleep(500);
                    }
                    if (steps == FingerprintDb.Steps)
                    {
                        MessageBox.Show("Huella Tomada, procesando", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // We have a fingerrpint template.
                        var template = db.GenerateTemplate();
                        if (template != null)
                        {
                            // turn template to base64 string
                            string templateStr = Convert.ToBase64String(template);
                            Fingerprint newFingerPrint = new Fingerprint(idString, 2, templateStr, "13");
                            // add the fp
                            if (!ACDevice.WriteFingerprint(newFingerPrint))
                            {
                                MessageBox.Show("No se pudo agregar la huella.", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Huella tomada exitosamente.", "Instrucción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    // free the memory
                    db.Free();
                }
                else
                {
                    Console.WriteLine("Error al inicializar la base de datos.");
                }
            }
        }

        private void btnFinger_Click(object sender, EventArgs e)
        {
            StopLogging();
            if (!ACDevice.IsConnected())
            {
                ACDevice.Connect("10.0.0.240", 4370, 0, 5000);
            }
            else
            {
                string idString = Interaction.InputBox("Introduzca el ID del usuario:", "Entrada", "");
                if (!string.IsNullOrEmpty(idString))
                {
                    FingerUser(idString);
                }
                else
                {
                    MessageBox.Show("Operación cancelada por el usuario.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                StartLogging();
            }
        }

        private void btnDoor_Click(object sender, EventArgs e)
        {
            StopLogging();
            if (!ACDevice.IsConnected())
            {
                ACDevice.Connect("10.0.0.240", 4370, 0, 5000);
            }

            else
            {
                string seconds = Interaction.InputBox("Introduzca los segundos de apertura:", "Parámetros", "");

                // Check if the user clicked "Cancel" or closed the input box
                if (!string.IsNullOrEmpty(seconds))
                {
                    // Try to parse the input as an integer
                    if (int.TryParse(seconds, out int openSeconds))
                    {
                        // Call the OpenDoor method and check the result
                        if (!ACDevice.OpenDoor(1, openSeconds))
                        {
                            MessageBox.Show("La puerta no se pudo abrir...", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StartLogging();
                            return; // Could not open door
                        }
                        else
                        {
                            MessageBox.Show($"Puerta abierta por {openSeconds} segundos.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StartLogging();
                        }
                    }
                    else
                    {
                        // Handle the case where the input is not a valid integer
                        MessageBox.Show("Por favor, introduzca un número válido de segundos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Handle the case where the user canceled the input
                    MessageBox.Show("Operación cancelada por el usuario.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void StartLogging()
        {
            // Use a cancellation token to gracefully stop logging
            loggingCancellationTokenSource = new CancellationTokenSource();

            // Start a background task for logging
            await Task.Run(() => LogEvents(loggingCancellationTokenSource.Token), loggingCancellationTokenSource.Token);
        }

        private void StopLogging()
        {
            // Cancel the logging task gracefully
            loggingCancellationTokenSource?.Cancel();
        }

        private async Task LogEvents(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                AccessPanelEvent accessPanelEvent = ACDevice.GetEventLog();
                AccessPanelRtEvent[] rtEvents = accessPanelEvent.Events;

                foreach (var rtEvent in rtEvents)
                {
                    DateTime eventTime;
                    if (DateTime.TryParse(rtEvent.Time, out eventTime))
                    {
                        string formattedTime = eventTime.ToString("yyyy-MM-ddTHH:mm:sszzz");

                        var eventData = new
                        {
                            time = formattedTime,
                            pin = rtEvent.Pin,
                            card = 0,
                            door = rtEvent.Door,
                            event_type = rtEvent.EventType
                        };

                        // Convert the object to JSON
                        string jsonEventData = JsonConvert.SerializeObject(eventData, Formatting.Indented);

                        // Post the event to your API
                        await PostEventToApi(jsonEventData);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid time format: {rtEvent.Time}");
                    }
                }

                // Add a delay between consecutive log checks to avoid constant polling
                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }

        private async Task PostEventToApi(string eventData)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(eventData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://www.naturalfitnessclubpos.com/api/event/", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Event posted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to post event. Status code: {response.StatusCode}");
                }
            }
        }

        private void btnStartLogging_Click(object sender, EventArgs e)
        {
            if (loggingCancellationTokenSource == null || loggingCancellationTokenSource.IsCancellationRequested)
            {
                StartLogging();
                MessageBox.Show("Ha empezado el monitoreo.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El monitoreo ya está en proceso.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task RenewUser()
        {
            string apiUrl = "https://www.naturalfitnessclubpos.com/api/renewed-members/";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var members = JsonConvert.DeserializeObject<List<MemberData>>(responseBody);

                    if (members.Count > 0)
                    {
                        MemberData firstMember = members[0];
                        int id = 0;
                        if (firstMember.oldId == null)
                        {
                            id = firstMember.Id;
                        }
                        else
                        {
                            id = Convert.ToInt32(firstMember.oldId);
                        }
                        string idString = id.ToString();

                        Console.WriteLine("Connected");

                        List<PullSDK_core.User> users = ACDevice.ReadUsers();
                        if (users == null)
                        {
                            Console.WriteLine("couldt read");
                            return;
                        }
                        foreach (PullSDK_core.User user in users)
                        {
                            if (user.Pin == idString)
                            {
                                // Get today's date
                                DateTime today = DateTime.Today;
                                // Calculate the end date as 31 days from today
                                DateTime endDate = today.AddDays(31);

                                // Format both dates as yyyymmdd
                                string startDateFormatted = today.ToString("yyyyMMdd");
                                string endDateFormatted = endDate.ToString("yyyyMMdd");

                                user.StartTime = startDateFormatted;
                                // Set EndTime to the reformatted expiration date
                                user.EndTime = endDateFormatted;
                            }                            
                        }                        
                    }
                }
            }
        }

        private async void btnMemberShip_Click(object sender, EventArgs e)
        {
            StopLogging();
            if (!ACDevice.IsConnected())
            {
                ACDevice.Connect("10.0.0.240", 4370, 0, 5000);
            }

            else
            {
                await RenewUser();
                MessageBox.Show("Dispositivo sincronizado.", "Estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartLogging();
            }
        }

        public class MemberData
        {
            public int Id { get; set; }
            public int? oldId { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NumeroTarjeta { get; set; }
            public int NumeroDepartamento { get; set; }
            public string Departamento { get; set; }
            public string Genero { get; set; }
            public int HuellasV10 { get; set; }
            public int HuellasV9 { get; set; }
            public bool TieneMembresia { get; set; }
            public DateTime? ExpiracionMembresia { get; set; }
        }

    }
}