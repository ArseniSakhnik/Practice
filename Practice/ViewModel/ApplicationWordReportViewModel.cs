using Practice.MVVMModels;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using Practice.Commands;
using System.Reflection;
using System.IO;
using Models;
using DatabaseConnector;

namespace Practice.ViewModel
{
    public class ApplicationWordReportViewModel : MVVMModel
    {

        public string note = "";

        public string Note
        {
            get => note;
            set => note = value;
        }


        private RelayCommand makeAReportCommand;

        public RelayCommand MakeAReportCommand
        {
            get
            {
                return makeAReportCommand ??
                    (makeAReportCommand = new RelayCommand(obj =>
                    {
                        string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\report.docx";
                        string text;
                        using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Create(filepath, WordprocessingDocumentType.Document))
                        {
                            MainDocumentPart mainDocumentPart = wordprocessingDocument.AddMainDocumentPart();
                            mainDocumentPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                            Body body = mainDocumentPart.Document.AppendChild(new Body());



                            Paragraph paragraph = body.AppendChild(new Paragraph());
                            Run run = paragraph.AppendChild(new Run());
                            //run.AppendChild(new Text(text));
                            //run.AppendChild(new Break());
                            //run.AppendChild(new Text(text));

                            text = "Программой могут пользоваться следующие пользователи: ";
                            run.AppendChild(new Text(text));
                            run.AppendChild(new Break());

                            List<User> users = UserService.GetUsers();

                            foreach(User u in users)
                            {  
                                run.AppendChild(new Text($@"    {u.UserName} с логином {u.Login} имеет следующие привилегии: {u.Role.Name}") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                            }

                            List<Organization> organizations = OrganizationService.GetOrganizations();
                            run.AppendChild(new Text("Организации, находящиеся в базе данных"));
                            run.AppendChild(new Break());

                            foreach(Organization o in organizations)
                            {
                                run.AppendChild(new Text($@"    В организации {o.OrganizationName} состоят следующие ученые: ") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                List<Scientist> scientists0 = OrganizationService.GetScientists(o);
                                foreach(Scientist s in scientists0)
                                {
                                    run.AppendChild(new Text($@"        {s.Name} {s.LastName}") {Space = SpaceProcessingModeValues.Preserve });
                                    run.AppendChild(new Break());
                                }
                            }
                            List<Conference> conferences = ConferenceService.GetConferences();
                            run.AppendChild(new Text("Конференции: "));
                            run.AppendChild(new Break());
                            foreach(Conference c in conferences)
                            {
                                run.AppendChild(new Text($@"    Конференция {c.ConferenceName}") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                run.AppendChild(new Text($@"    Дата проведения: {c.StartOfConference}") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                run.AppendChild(new Text($@"    Описание: {c.ConferenceDescription}") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                run.AppendChild(new Text($@"    Место проведения: {c.Location.LocationName} в стране {c.Location.Country.CountryName}") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                run.AppendChild(new Text($@"    В конференции учавствуют сладующие ученые: ") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                List<Scientist> scientists1 = ConferenceService.GetScientists(c);
                                foreach(Scientist s in scientists1)
                                {
                                    run.AppendChild(new Text($@"         {s.Name} {s.LastName}") { Space = SpaceProcessingModeValues.Preserve });
                                    run.AppendChild(new Break());
                                }
                            }
                            run.AppendChild(new Text($"Научные сотрудники: "));
                            run.AppendChild(new Break());
                            List<Scientist> scientists = ScientistService.GetScientists();
                            foreach(Scientist s in scientists)
                            {
                                run.AppendChild(new Text($@"     {s.Name} {s.LastName} из страны {s.Country.CountryName} ") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                run.AppendChild(new Text($@"     Доклады научного сотрудника: ") { Space = SpaceProcessingModeValues.Preserve });
                                run.AppendChild(new Break());
                                foreach(Report r in s.Reports)
                                {
                                    run.AppendChild(new Text($"         {r.ReportName} с датой написания {r.ReportDate}") { Space = SpaceProcessingModeValues.Preserve });
                                    run.AppendChild(new Break());
                                }
                            }

                            run.AppendChild(new Text(note));
                        }
                    }));
            }
        }
    }

}
