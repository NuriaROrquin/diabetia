using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace Diabetia.API
{
    public partial class diabetiaContext : DbContext
    {
        public diabetiaContext()
        {
        }

        public diabetiaContext(DbContextOptions<diabetiaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActividadFisica> ActividadFisicas { get; set; } = null!;
        public virtual DbSet<CargaEvento> CargaEventos { get; set; } = null!;
        public virtual DbSet<DiaSemana> DiaSemanas { get; set; } = null!;
        public virtual DbSet<Dispositivo> Dispositivos { get; set; } = null!;
        public virtual DbSet<DispositivoPaciente> DispositivoPacientes { get; set; } = null!;
        public virtual DbSet<EncargadoLegal> EncargadoLegals { get; set; } = null!;
        public virtual DbSet<Enfermedad> Enfermedads { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidads { get; set; } = null!;
        public virtual DbSet<EventoActividadFisica> EventoActividadFisicas { get; set; } = null!;
        public virtual DbSet<EventoComidum> EventoComida { get; set; } = null!;
        public virtual DbSet<EventoEstudio> EventoEstudios { get; set; } = null!;
        public virtual DbSet<EventoGlucosa> EventoGlucosas { get; set; } = null!;
        public virtual DbSet<EventoInsulina> EventoInsulinas { get; set; } = null!;
        public virtual DbSet<EventoSalud> EventoSaluds { get; set; } = null!;
        public virtual DbSet<EventoVisitaMedica> EventoVisitaMedicas { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<GravedadSintoma> GravedadSintomas { get; set; } = null!;
        public virtual DbSet<Ingrediente> Ingredientes { get; set; } = null!;
        public virtual DbSet<IngredienteComidum> IngredienteComida { get; set; } = null!;
        public virtual DbSet<InsulinaPaciente> InsulinaPacientes { get; set; } = null!;
        public virtual DbSet<MedidaTomadaEventoSalud> MedidaTomadaEventoSaluds { get; set; } = null!;
        public virtual DbSet<MedidaTomadaSintoma> MedidaTomadaSintomas { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<PacienteActividadFisica> PacienteActividadFisicas { get; set; } = null!;
        public virtual DbSet<PacienteEnfermedadPreexistente> PacienteEnfermedadPreexistentes { get; set; } = null!;
        public virtual DbSet<Profesional> Profesionals { get; set; } = null!;
        public virtual DbSet<Recordatorio> Recordatorios { get; set; } = null!;
        public virtual DbSet<RecordatorioDium> RecordatorioDia { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SensibilidadInsulina> SensibilidadInsulinas { get; set; } = null!;
        public virtual DbSet<Sentimiento> Sentimientos { get; set; } = null!;
        public virtual DbSet<Sintoma> Sintomas { get; set; } = null!;
        public virtual DbSet<SintomaEventoSalud> SintomaEventoSaluds { get; set; } = null!;
        public virtual DbSet<TipoAccionInsulina> TipoAccionInsulinas { get; set; } = null!;
        public virtual DbSet<TipoCargaComidum> TipoCargaComida { get; set; } = null!;
        public virtual DbSet<TipoDiabete> TipoDiabetes { get; set; } = null!;
        public virtual DbSet<TipoEvento> TipoEventos { get; set; } = null!;
        public virtual DbSet<TipoInsulina> TipoInsulinas { get; set; } = null!;
        public virtual DbSet<UnidadMedidaIngrediente> UnidadMedidaIngredientes { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<VinculoProfesionalPaciente> VinculoProfesionalPacientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=diabetia;user=root;password=;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<ActividadFisica>(entity =>
            {
                entity.ToTable("actividad_fisica");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<CargaEvento>(entity =>
            {
                entity.ToTable("carga_evento");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.HasIndex(e => e.IdTipoEvento, "id_tipo_evento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EsNotaLibre)
                    .HasColumnName("es_nota_libre")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FechaActual)
                    .HasColumnType("timestamp")
                    .HasColumnName("fecha_actual")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FechaEvento)
                    .HasColumnType("timestamp")
                    .HasColumnName("fecha_evento");

                entity.Property(e => e.FueRealizado)
                    .HasColumnName("fue_realizado")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.IdTipoEvento).HasColumnName("id_tipo_evento");

                entity.Property(e => e.NotaLibre)
                    .HasMaxLength(255)
                    .HasColumnName("nota_libre");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.CargaEventos)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carga_evento_ibfk_1");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.CargaEventos)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("carga_evento_ibfk_2");
            });

            modelBuilder.Entity<DiaSemana>(entity =>
            {
                entity.ToTable("dia_semana");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dia)
                    .HasMaxLength(20)
                    .HasColumnName("dia");
            });

            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.ToTable("dispositivo");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Marca)
                    .HasMaxLength(100)
                    .HasColumnName("marca");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<DispositivoPaciente>(entity =>
            {
                entity.ToTable("dispositivo_paciente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdDispositivo, "id_dispositivo");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");

                entity.Property(e => e.IdDispositivo).HasColumnName("id_dispositivo");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.HasOne(d => d.IdDispositivoNavigation)
                    .WithMany(p => p.DispositivoPacientes)
                    .HasForeignKey(d => d.IdDispositivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dispositivo_paciente_ibfk_2");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.DispositivoPacientes)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dispositivo_paciente_ibfk_1");
            });

            modelBuilder.Entity<EncargadoLegal>(entity =>
            {
                entity.ToTable("encargado_legal");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.PuedeEditarDatos).HasColumnName("puede_editar_datos");

                entity.Property(e => e.RelacionConPaciente)
                    .HasMaxLength(100)
                    .HasColumnName("relacion_con_paciente");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.EncargadoLegals)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("encargado_legal_ibfk_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.EncargadoLegals)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("encargado_legal_ibfk_2");
            });

            modelBuilder.Entity<Enfermedad>(entity =>
            {
                entity.ToTable("enfermedad");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EsCronica).HasColumnName("es_cronica");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.ToTable("especialidad");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NombreEspecialidad)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_especialidad");
            });

            modelBuilder.Entity<EventoActividadFisica>(entity =>
            {
                entity.ToTable("evento_actividad_fisica");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdActividadRegistrada, "id_actividad_registrada");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.IdActividadRegistrada).HasColumnName("id_actividad_registrada");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.HasOne(d => d.IdActividadRegistradaNavigation)
                    .WithMany(p => p.EventoActividadFisicas)
                    .HasForeignKey(d => d.IdActividadRegistrada)
                    .HasConstraintName("evento_actividad_fisica_ibfk_2");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoActividadFisicas)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_actividad_fisica_ibfk_1");
            });

            modelBuilder.Entity<EventoComidum>(entity =>
            {
                entity.ToTable("evento_comida");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdTipoCargaComida, "id_tipo_carga_comida");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carbohidratos)
                    .HasPrecision(10, 2)
                    .HasColumnName("carbohidratos");

                entity.Property(e => e.FibraAlimentaria)
                    .HasPrecision(10, 2)
                    .HasColumnName("fibra_alimentaria");

                entity.Property(e => e.FueHipo).HasColumnName("fue_hipo");

                entity.Property(e => e.GrasasTotales)
                    .HasPrecision(10, 2)
                    .HasColumnName("grasas_totales");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdTipoCargaComida).HasColumnName("id_tipo_carga_comida");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(255)
                    .HasColumnName("imagen");

                entity.Property(e => e.Proteinas)
                    .HasPrecision(10, 2)
                    .HasColumnName("proteinas");

                entity.Property(e => e.Sodio)
                    .HasPrecision(10, 2)
                    .HasColumnName("sodio");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoComida)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_comida_ibfk_1");

                entity.HasOne(d => d.IdTipoCargaComidaNavigation)
                    .WithMany(p => p.EventoComida)
                    .HasForeignKey(d => d.IdTipoCargaComida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_comida_ibfk_2");
            });

            modelBuilder.Entity<EventoEstudio>(entity =>
            {
                entity.ToTable("evento_estudio");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdProfesional, "id_profesional");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(255)
                    .HasColumnName("archivo");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

                entity.Property(e => e.TipoEstudio)
                    .HasMaxLength(100)
                    .HasColumnName("tipo_estudio");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoEstudios)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_estudio_ibfk_2");

                entity.HasOne(d => d.IdProfesionalNavigation)
                    .WithMany(p => p.EventoEstudios)
                    .HasForeignKey(d => d.IdProfesional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_estudio_ibfk_1");
            });

            modelBuilder.Entity<EventoGlucosa>(entity =>
            {
                entity.ToTable("evento_glucosa");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdDispositivoPaciente, "id_dispositivo_paciente");

                entity.HasIndex(e => e.IdEventoComida, "id_evento_comida");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Glucemia)
                    .HasPrecision(10, 2)
                    .HasColumnName("glucemia");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdDispositivoPaciente).HasColumnName("id_dispositivo_paciente");

                entity.Property(e => e.IdEventoComida).HasColumnName("id_evento_comida");

                entity.Property(e => e.MedicionPostComida).HasColumnName("medicion_post_comida");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoGlucosas)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_glucosa_ibfk_1");

                entity.HasOne(d => d.IdDispositivoPacienteNavigation)
                    .WithMany(p => p.EventoGlucosas)
                    .HasForeignKey(d => d.IdDispositivoPaciente)
                    .HasConstraintName("evento_glucosa_ibfk_2");

                entity.HasOne(d => d.IdEventoComidaNavigation)
                    .WithMany(p => p.EventoGlucosas)
                    .HasForeignKey(d => d.IdEventoComida)
                    .HasConstraintName("evento_glucosa_ibfk_3");
            });

            modelBuilder.Entity<EventoInsulina>(entity =>
            {
                entity.ToTable("evento_insulina");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdEventoComida, "id_evento_comida");

                entity.HasIndex(e => e.IdInsulinaPaciente, "id_insulina_paciente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdEventoComida).HasColumnName("id_evento_comida");

                entity.Property(e => e.IdInsulinaPaciente).HasColumnName("id_insulina_paciente");

                entity.Property(e => e.InsulinaInyectada).HasColumnName("insulina_inyectada");

                entity.Property(e => e.InsulinaPreComida).HasColumnName("insulina_pre_comida");

                entity.Property(e => e.InsulinaRecomendada).HasColumnName("insulina_recomendada");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoInsulinas)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_insulina_ibfk_1");

                entity.HasOne(d => d.IdEventoComidaNavigation)
                    .WithMany(p => p.EventoInsulinas)
                    .HasForeignKey(d => d.IdEventoComida)
                    .HasConstraintName("evento_insulina_ibfk_3");

                entity.HasOne(d => d.IdInsulinaPacienteNavigation)
                    .WithMany(p => p.EventoInsulinas)
                    .HasForeignKey(d => d.IdInsulinaPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_insulina_ibfk_2");
            });

            modelBuilder.Entity<EventoSalud>(entity =>
            {
                entity.ToTable("evento_salud");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdEnfermedad, "id_enfermedad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdEnfermedad).HasColumnName("id_enfermedad");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoSaluds)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_salud_ibfk_1");

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.EventoSaluds)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_salud_ibfk_2");
            });

            modelBuilder.Entity<EventoVisitaMedica>(entity =>
            {
                entity.ToTable("evento_visita_medica");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdProfesional, "id_profesional");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.EventoVisitaMedicas)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_visita_medica_ibfk_2");

                entity.HasOne(d => d.IdProfesionalNavigation)
                    .WithMany(p => p.EventoVisitaMedicas)
                    .HasForeignKey(d => d.IdProfesional)
                    .HasConstraintName("evento_visita_medica_ibfk_1");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdCargaEvento, "id_carga_evento");

                entity.HasIndex(e => e.IdSentimiento, "id_sentimiento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FueRealizado)
                    .HasColumnName("fue_realizado")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HoraAviso)
                    .HasColumnType("timestamp")
                    .HasColumnName("hora_aviso");

                entity.Property(e => e.IdCargaEvento).HasColumnName("id_carga_evento");

                entity.Property(e => e.IdSentimiento).HasColumnName("id_sentimiento");

                entity.Property(e => e.NotaLibre)
                    .HasMaxLength(255)
                    .HasColumnName("nota_libre");

                entity.HasOne(d => d.IdCargaEventoNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.IdCargaEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("feedback_ibfk_1");

                entity.HasOne(d => d.IdSentimientoNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.IdSentimiento)
                    .HasConstraintName("feedback_ibfk_2");
            });

            modelBuilder.Entity<GravedadSintoma>(entity =>
            {
                entity.ToTable("gravedad_sintoma");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.ToTable("ingrediente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdUnidadMedidaIngrediente, "id_unidad_medida_ingrediente");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Carbohidratos)
                    .HasPrecision(10, 2)
                    .HasColumnName("carbohidratos");

                entity.Property(e => e.FibraAlimentaria)
                    .HasPrecision(10, 2)
                    .HasColumnName("fibra_alimentaria");

                entity.Property(e => e.FueChequeado)
                    .HasColumnName("fue_chequeado")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.GrasasTotales)
                    .HasPrecision(10, 2)
                    .HasColumnName("grasas_totales");

                entity.Property(e => e.IdUnidadMedidaIngrediente).HasColumnName("id_unidad_medida_ingrediente");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Kilocalorias)
                    .HasPrecision(10, 2)
                    .HasColumnName("kilocalorias");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .HasColumnName("nombre");

                entity.Property(e => e.PorcionEstandar)
                    .HasPrecision(10, 2)
                    .HasColumnName("porcion_estandar");

                entity.Property(e => e.Proteinas)
                    .HasPrecision(10, 2)
                    .HasColumnName("proteinas");

                entity.Property(e => e.Sodio)
                    .HasPrecision(10, 2)
                    .HasColumnName("sodio");

                entity.HasOne(d => d.IdUnidadMedidaIngredienteNavigation)
                    .WithMany(p => p.Ingredientes)
                    .HasForeignKey(d => d.IdUnidadMedidaIngrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingrediente_ibfk_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Ingredientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("ingrediente_ibfk_2");
            });

            modelBuilder.Entity<IngredienteComidum>(entity =>
            {
                entity.ToTable("ingrediente_comida");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdEventoComida, "id_evento_comida");

                entity.HasIndex(e => e.IdIngrediente, "id_ingrediente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CantidadIngerida).HasColumnName("cantidad_ingerida");

                entity.Property(e => e.Carbohidratos)
                    .HasPrecision(10, 2)
                    .HasColumnName("carbohidratos");

                entity.Property(e => e.FibraAlimentaria)
                    .HasPrecision(10, 2)
                    .HasColumnName("fibra_alimentaria");

                entity.Property(e => e.GrasasTotales)
                    .HasPrecision(10, 2)
                    .HasColumnName("grasas_totales");

                entity.Property(e => e.IdEventoComida).HasColumnName("id_evento_comida");

                entity.Property(e => e.IdIngrediente).HasColumnName("id_ingrediente");

                entity.Property(e => e.Proteinas)
                    .HasPrecision(10, 2)
                    .HasColumnName("proteinas");

                entity.Property(e => e.Sodio)
                    .HasPrecision(10, 2)
                    .HasColumnName("sodio");

                entity.HasOne(d => d.IdEventoComidaNavigation)
                    .WithMany(p => p.IngredienteComida)
                    .HasForeignKey(d => d.IdEventoComida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingrediente_comida_ibfk_2");

                entity.HasOne(d => d.IdIngredienteNavigation)
                    .WithMany(p => p.IngredienteComida)
                    .HasForeignKey(d => d.IdIngrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingrediente_comida_ibfk_1");
            });

            modelBuilder.Entity<InsulinaPaciente>(entity =>
            {
                entity.ToTable("insulina_paciente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.HasIndex(e => e.IdTipoInsulina, "id_tipo_insulina");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.IdTipoInsulina).HasColumnName("id_tipo_insulina");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.InsulinaPacientes)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("insulina_paciente_ibfk_1");

                entity.HasOne(d => d.IdTipoInsulinaNavigation)
                    .WithMany(p => p.InsulinaPacientes)
                    .HasForeignKey(d => d.IdTipoInsulina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("insulina_paciente_ibfk_2");
            });

            modelBuilder.Entity<MedidaTomadaEventoSalud>(entity =>
            {
                entity.ToTable("medida_tomada_evento_salud");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdEventoSalud, "id_evento_salud");

                entity.HasIndex(e => e.IdMedidaTomada, "id_medida_tomada");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEventoSalud).HasColumnName("id_evento_salud");

                entity.Property(e => e.IdMedidaTomada).HasColumnName("id_medida_tomada");

                entity.HasOne(d => d.IdEventoSaludNavigation)
                    .WithMany(p => p.MedidaTomadaEventoSaluds)
                    .HasForeignKey(d => d.IdEventoSalud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("medida_tomada_evento_salud_ibfk_1");

                entity.HasOne(d => d.IdMedidaTomadaNavigation)
                    .WithMany(p => p.MedidaTomadaEventoSaluds)
                    .HasForeignKey(d => d.IdMedidaTomada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("medida_tomada_evento_salud_ibfk_2");
            });

            modelBuilder.Entity<MedidaTomadaSintoma>(entity =>
            {
                entity.ToTable("medida_tomada_sintoma");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("paciente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdSensibilidadInsulina, "id_sensibilidad_insulina");

                entity.HasIndex(e => e.IdTipoDiabetes, "id_tipo_diabetes");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Altura).HasColumnName("altura");

                entity.Property(e => e.CorreccionCh).HasColumnName("correccion_ch");

                entity.Property(e => e.FechaDiagnostico).HasColumnName("fecha_diagnostico");

                entity.Property(e => e.IdSensibilidadInsulina).HasColumnName("id_sensibilidad_insulina");

                entity.Property(e => e.IdTipoDiabetes).HasColumnName("id_tipo_diabetes");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.MaxGlucosa).HasColumnName("max_glucosa");

                entity.Property(e => e.MinGlucosa).HasColumnName("min_glucosa");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.UsaInsulina).HasColumnName("usa_insulina");

                entity.HasOne(d => d.IdSensibilidadInsulinaNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdSensibilidadInsulina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_ibfk_3");

                entity.HasOne(d => d.IdTipoDiabetesNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdTipoDiabetes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_ibfk_1");
            });

            modelBuilder.Entity<PacienteActividadFisica>(entity =>
            {
                entity.ToTable("paciente_actividad_fisica");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdActividadFisica, "id_actividad_fisica");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.Frecuencia).HasColumnName("frecuencia");

                entity.Property(e => e.IdActividadFisica).HasColumnName("id_actividad_fisica");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.HasOne(d => d.IdActividadFisicaNavigation)
                    .WithMany(p => p.PacienteActividadFisicas)
                    .HasForeignKey(d => d.IdActividadFisica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_actividad_fisica_ibfk_2");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.PacienteActividadFisicas)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_actividad_fisica_ibfk_1");
            });

            modelBuilder.Entity<PacienteEnfermedadPreexistente>(entity =>
            {
                entity.ToTable("paciente_enfermedad_preexistente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdEnfermedad, "id_enfermedad");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEnfermedad).HasColumnName("id_enfermedad");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.PacienteEnfermedadPreexistentes)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_enfermedad_preexistente_ibfk_2");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.PacienteEnfermedadPreexistentes)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("paciente_enfermedad_preexistente_ibfk_1");
            });

            modelBuilder.Entity<Profesional>(entity =>
            {
                entity.ToTable("profesional");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdEspecialidad, "id_especialidad");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.HasIndex(e => e.Matricula, "matricula")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Matricula).HasColumnName("matricula");

                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Profesionals)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profesional_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Profesionals)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("profesional_ibfk_1");
            });

            modelBuilder.Entity<Recordatorio>(entity =>
            {
                entity.ToTable("recordatorio");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdTipoEvento, "id_tipo_evento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EstaActivo)
                    .HasColumnName("esta_activo")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FechaEliminacion)
                    .HasColumnType("timestamp")
                    .HasColumnName("fecha_eliminacion");

                entity.Property(e => e.FechaFinalizacion).HasColumnName("fecha_finalizacion");

                entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");

                entity.Property(e => e.FueEliminado)
                    .HasColumnName("fue_eliminado")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HorarioActividad)
                    .HasColumnType("time")
                    .HasColumnName("horario_actividad");

                entity.Property(e => e.IdTipoEvento).HasColumnName("id_tipo_evento");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.Recordatorios)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordatorio_ibfk_1");
            });

            modelBuilder.Entity<RecordatorioDium>(entity =>
            {
                entity.ToTable("recordatorio_dia");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdDiaSemana, "id_dia_semana");

                entity.HasIndex(e => e.IdRecordatorio, "id_recordatorio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaHoraRecordatorio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hora_recordatorio");

                entity.Property(e => e.IdDiaSemana).HasColumnName("id_dia_semana");

                entity.Property(e => e.IdRecordatorio).HasColumnName("id_recordatorio");

                entity.HasOne(d => d.IdDiaSemanaNavigation)
                    .WithMany(p => p.RecordatorioDia)
                    .HasForeignKey(d => d.IdDiaSemana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordatorio_dia_ibfk_2");

                entity.HasOne(d => d.IdRecordatorioNavigation)
                    .WithMany(p => p.RecordatorioDia)
                    .HasForeignKey(d => d.IdRecordatorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recordatorio_dia_ibfk_1");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Rol1)
                    .HasMaxLength(50)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<SensibilidadInsulina>(entity =>
            {
                entity.ToTable("sensibilidad_insulina");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nivel)
                    .HasMaxLength(50)
                    .HasColumnName("nivel");
            });

            modelBuilder.Entity<Sentimiento>(entity =>
            {
                entity.ToTable("sentimiento");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Emoji)
                    .HasMaxLength(255)
                    .HasColumnName("emoji");

                entity.Property(e => e.Sentimiento1)
                    .HasMaxLength(50)
                    .HasColumnName("sentimiento");
            });

            modelBuilder.Entity<Sintoma>(entity =>
            {
                entity.ToTable("sintoma");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<SintomaEventoSalud>(entity =>
            {
                entity.ToTable("sintoma_evento_salud");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdEventoSalud, "id_evento_salud");

                entity.HasIndex(e => e.IdGravedadSintoma, "id_gravedad_sintoma");

                entity.HasIndex(e => e.IdSintoma, "id_sintoma");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdEventoSalud).HasColumnName("id_evento_salud");

                entity.Property(e => e.IdGravedadSintoma).HasColumnName("id_gravedad_sintoma");

                entity.Property(e => e.IdSintoma).HasColumnName("id_sintoma");

                entity.HasOne(d => d.IdEventoSaludNavigation)
                    .WithMany(p => p.SintomaEventoSaluds)
                    .HasForeignKey(d => d.IdEventoSalud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sintoma_evento_salud_ibfk_1");

                entity.HasOne(d => d.IdGravedadSintomaNavigation)
                    .WithMany(p => p.SintomaEventoSaluds)
                    .HasForeignKey(d => d.IdGravedadSintoma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sintoma_evento_salud_ibfk_3");

                entity.HasOne(d => d.IdSintomaNavigation)
                    .WithMany(p => p.SintomaEventoSaluds)
                    .HasForeignKey(d => d.IdSintoma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sintoma_evento_salud_ibfk_2");
            });

            modelBuilder.Entity<TipoAccionInsulina>(entity =>
            {
                entity.ToTable("tipo_accion_insulina");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TipoAccion)
                    .HasMaxLength(50)
                    .HasColumnName("tipo_accion");
            });

            modelBuilder.Entity<TipoCargaComidum>(entity =>
            {
                entity.ToTable("tipo_carga_comida");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TipoCarga)
                    .HasMaxLength(20)
                    .HasColumnName("tipo_carga");
            });

            modelBuilder.Entity<TipoDiabete>(entity =>
            {
                entity.ToTable("tipo_diabetes");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.ToTable("tipo_evento");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NombreTabla)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_tabla");

                entity.Property(e => e.TiempoFeedback).HasColumnName("tiempo_feedback");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TipoInsulina>(entity =>
            {
                entity.ToTable("tipo_insulina");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdTipoAccionInsulina, "id_tipo_accion_insulina");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Duracion).HasColumnName("duracion");

                entity.Property(e => e.IdTipoAccionInsulina).HasColumnName("id_tipo_accion_insulina");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdTipoAccionInsulinaNavigation)
                    .WithMany(p => p.TipoInsulinas)
                    .HasForeignKey(d => d.IdTipoAccionInsulina)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_insulina_ibfk_1");
            });

            modelBuilder.Entity<UnidadMedidaIngrediente>(entity =>
            {
                entity.ToTable("unidad_medida_ingrediente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abreviacion)
                    .HasMaxLength(10)
                    .HasColumnName("abreviacion");

                entity.Property(e => e.Medida)
                    .HasMaxLength(50)
                    .HasColumnName("medida");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Dni, "dni")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.HasIndex(e => e.IdRol, "id_rol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .HasColumnName("dni");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.EstaActivo)
                    .HasColumnName("esta_activo")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Genero)
                    .HasMaxLength(1)
                    .HasColumnName("genero")
                    .IsFixedLength();

                entity.Property(e => e.Hash)
                    .HasMaxLength(100)
                    .HasColumnName("hash");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.Pais)
                    .HasMaxLength(255)
                    .HasColumnName("pais");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(30)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_ibfk_1");
            });

            modelBuilder.Entity<VinculoProfesionalPaciente>(entity =>
            {
                entity.ToTable("vinculo_profesional_paciente");

                entity.UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.IdPaciente, "id_paciente");

                entity.HasIndex(e => e.IdProfesional, "id_profesional");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.IdProfesional).HasColumnName("id_profesional");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.VinculoProfesionalPacientes)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vinculo_profesional_paciente_ibfk_1");

                entity.HasOne(d => d.IdProfesionalNavigation)
                    .WithMany(p => p.VinculoProfesionalPacientes)
                    .HasForeignKey(d => d.IdProfesional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vinculo_profesional_paciente_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
