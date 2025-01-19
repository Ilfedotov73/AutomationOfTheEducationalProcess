using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.storage_contracts;
using contracts.worker_contracts;
using data_base_implement.implemnts;
using interactors;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using presenter;
using worker.implements;
using worker.office_package;
using worker.office_package.office_implements;

namespace WebApplicationRestAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddLogging(option => { option.SetMinimumLevel(LogLevel.Information); option.AddNLog("nlog.config"); });
            // ------STORAGE------
            builder.Services.AddTransient<Idepartment_storage, db_department_storage>();
            builder.Services.AddTransient<Idirection_storage, db_direction_storage>();
            builder.Services.AddTransient<Idocument_storage, db_document_storage>();
            builder.Services.AddTransient<Ifaculty_storage, db_faculty_storage>();
            builder.Services.AddTransient<Istudent_group_storage, db_student_group_storage>();
            builder.Services.AddTransient<Istudent_storage, db_student_storage>();
            builder.Services.AddTransient<Itemplate_storage, db_template_storage>();
            builder.Services.AddTransient<Iuser_storage, db_user_storage>();

            // ------INTERACTORS------
            builder.Services.AddTransient<Idepartment_logic, department_logic>();
            builder.Services.AddTransient<Idirection_logic, direction_logic>();
            builder.Services.AddSingleton<Idocument_logic, document_logic>();
            builder.Services.AddTransient<Ifaculty_logic, faculty_logic>();
            builder.Services.AddTransient<Istudent_group_logic, student_group_logic>();
            builder.Services.AddTransient<Istudent_logic, student_logic>();
            builder.Services.AddTransient<Itemplate_logic, template_logic>();
            builder.Services.AddTransient<Iuser_logic, user_logic>();

            // ------WORKER------
            builder.Services.AddTransient<Itemplate_worker, itp_template_worker>();
            builder.Services.AddSingleton<Icreate_docx_file, create_to_docx>();
            builder.Services.AddSingleton<Icreate_xlsx_file, create_to_xlsx>();

            // ------PRESENTER------
            builder.Services.AddTransient<Idepartment_presenter, department_presenter>();
            builder.Services.AddTransient<Idirection_presenter, direction_presenter>();
            builder.Services.AddTransient<Idocument_presenter, document_presenter>();
            builder.Services.AddTransient<Ifaculty_presenter, faculty_presenter>();
            builder.Services.AddTransient<Istudent_group_presenter, student_group_presenter>();
            builder.Services.AddTransient<Istudent_presenter, student_presenter>();
            builder.Services.AddTransient<Itemplate_presenter, template_presenter>();
            builder.Services.AddTransient<Iuser_presenter, user_presenter>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo {
                Title = "AutomationOfTheEducationalProcessRestApi",
                Version = "V1"
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", 
                                "AutomationOfTheEducationalProcessRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
