using System;
using System.Collections.Generic;
using Npgsql;
using TP_XML.Models;

namespace TP_XML.Services
{
    public static class DatabaseSeeder
    {
        private static string ConnectionString => "Host=localhost;Port=5433;Username=admin;Password=admin;Database=DEV_SYSACAD";

        public static int ImportarTodo()
        {
            int errores = 0;
            try
            {
                CrearTablasSiNoExisten();
                AumentarTamanioCampoOrientacion();
                AumentarTamanioCampoMateria(); // <-- Agregado aquí

                errores += Insertar<Universidad>("Archivos/universidad.xml", "universidad", InsertUniversidad);
                errores += Insertar<Grado>("Archivos/grados.xml", "grado", InsertGrado);
                errores += Insertar<Facultad>("Archivos/facultades.xml", "facultad", InsertFacultad);
                errores += Insertar<Materia>("Archivos/materias.xml", "materia", InsertMateria);
                errores += Insertar<Especialidad>("Archivos/especialidades.xml", "especialidad", InsertEspecialidad);
                errores += Insertar<Orientacion>("Archivos/orientaciones.xml", "orientacion", InsertOrientacion);
                errores += Insertar<Plan>("Archivos/planes.xml", "plan", InsertPlan);
                errores += Insertar<Pais>("Archivos/paises.xml", "pais", InsertPais);
                errores += InsertarLocalidades("Archivos/localidades.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general en la importación: {ex.Message}");
                errores++;
            }
            return errores;
        }

        private static void AumentarTamanioCampoOrientacion()
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "ALTER TABLE orientacion ALTER COLUMN nombre TYPE VARCHAR(255);", conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Advertencia: No se pudo modificar el tamaño del campo 'nombre' en 'orientacion': {ex.Message}");
            }
        }

        private static void AumentarTamanioCampoMateria()
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                "ALTER TABLE materia ALTER COLUMN nombre TYPE VARCHAR(255);", conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Advertencia: No se pudo modificar el tamaño del campo 'nombre' en 'materia': {ex.Message}");
            }
        }

        private static void CrearTablasSiNoExisten()
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS universidad (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS grado (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS facultad (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS materia (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100),
                    especialidad INTEGER,
                    plan INTEGER,
                    ano INTEGER
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS especialidad (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS orientacion (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(255),
                    especialidad INTEGER,
                    plan INTEGER
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS plan (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100),
                    especialidad INTEGER
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS pais (
                    id INTEGER PRIMARY KEY,
                    nombre VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }

            using (var cmd = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS localidad (
                    codigo INTEGER PRIMARY KEY,
                    ciudad VARCHAR(100),
                    provincia VARCHAR(100),
                    pais_del_c VARCHAR(100)
                );", conn))
            { cmd.ExecuteNonQuery(); }
        }

        private static int Insertar<T>(string xmlPath, string tabla, Action<NpgsqlConnection, T> insertAction)
        {
            int errores = 0;
            var items = XmlImporter.ImportXml<T>(xmlPath);
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            foreach (var item in items)
            {
                try
                {
                    insertAction(conn, item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al insertar en {tabla}: {ex.Message}");
                    errores++;
                }
            }
            return errores;
        }

        private static int InsertarLocalidades(string xmlPath)
        {
            int errores = 0;
            var items = XmlImporter.ImportLocalidades(xmlPath);
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            foreach (var item in items)
            {
                try
                {
                    InsertLocalidad(conn, item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al insertar Localidad (codigo={item.Id}): {ex.Message}");
                    errores++;
                }
            }
            return errores;
        }

        private static void InsertUniversidad(NpgsqlConnection conn, Universidad u)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO universidad (id, nombre) VALUES (@id, @nombre) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre", conn);
                cmd.Parameters.AddWithValue("id", u.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)u.Nombre ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Universidad (id={u.Id}): {ex.Message}");
            }
        }

        private static void InsertGrado(NpgsqlConnection conn, Grado g)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO grado (id, nombre) VALUES (@id, @nombre) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre", conn);
                cmd.Parameters.AddWithValue("id", g.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)g.Nombre ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Grado (id={g.Id}): {ex.Message}");
            }
        }

        private static void InsertFacultad(NpgsqlConnection conn, Facultad f)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO facultad (id, nombre) VALUES (@id, @nombre) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre", conn);
                cmd.Parameters.AddWithValue("id", f.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)f.Nombre ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Facultad (id={f.Id}): {ex.Message}");
            }
        }

        private static void InsertMateria(NpgsqlConnection conn, Materia m)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO materia (id, nombre, especialidad, plan, ano) VALUES (@id, @nombre, @especialidad, @plan, @ano) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre, especialidad = EXCLUDED.especialidad, plan = EXCLUDED.plan, ano = EXCLUDED.ano", conn);
                cmd.Parameters.AddWithValue("id", m.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)m.Nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("especialidad", (object?)m.Especialidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("plan", (object?)m.Plan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("ano", (object?)m.Ano ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Materia (id={m.Id}): {ex.Message}");
            }
        }

        private static void InsertEspecialidad(NpgsqlConnection conn, Especialidad e)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO especialidad (id, nombre) VALUES (@id, @nombre) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre", conn);
                cmd.Parameters.AddWithValue("id", e.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)e.Nombre ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Especialidad (id={e.Id}): {ex.Message}");
            }
        }

        private static void InsertOrientacion(NpgsqlConnection conn, Orientacion o)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO orientacion (id, nombre, especialidad, plan) VALUES (@id, @nombre, @especialidad, @plan) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre, especialidad = EXCLUDED.especialidad, plan = EXCLUDED.plan", conn);
                cmd.Parameters.AddWithValue("id", o.Id);
                // Truncar a 255 caracteres para evitar error si el texto es muy largo
                cmd.Parameters.AddWithValue("nombre", (object?)(o.Nombre != null && o.Nombre.Length > 255 ? o.Nombre.Substring(0, 255) : o.Nombre) ?? DBNull.Value);
                cmd.Parameters.AddWithValue("especialidad", (object?)o.Especialidad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("plan", (object?)o.Plan ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Orientacion (id={o.Id}): {ex.Message}");
            }
        }

        private static void InsertPlan(NpgsqlConnection conn, Plan p)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO plan (id, nombre, especialidad) VALUES (@id, @nombre, @especialidad) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre, especialidad = EXCLUDED.especialidad", conn);
                cmd.Parameters.AddWithValue("id", p.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)p.Nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("especialidad", (object?)p.Especialidad ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Plan (id={p.Id}): {ex.Message}");
            }
        }

        private static void InsertPais(NpgsqlConnection conn, Pais p)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO pais (id, nombre) VALUES (@id, @nombre) " +
                    "ON CONFLICT (id) DO UPDATE SET nombre = EXCLUDED.nombre", conn);
                cmd.Parameters.AddWithValue("id", p.Id);
                cmd.Parameters.AddWithValue("nombre", (object?)p.Nombre ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Pais (id={p.Id}): {ex.Message}");
            }
        }

        private static void InsertLocalidad(NpgsqlConnection conn, Localidad l)
        {
            try
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO localidad (codigo, ciudad, provincia, pais_del_c) VALUES (@codigo, @ciudad, @provincia, @pais_del_c) " +
                    "ON CONFLICT (codigo) DO UPDATE SET ciudad = EXCLUDED.ciudad, provincia = EXCLUDED.provincia, pais_del_c = EXCLUDED.pais_del_c", conn);
                cmd.Parameters.AddWithValue("codigo", l.Id);
                cmd.Parameters.AddWithValue("ciudad", (object?)l.Ciudad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("provincia", (object?)l.Provincia ?? DBNull.Value);
                cmd.Parameters.AddWithValue("pais_del_c", (object?)l.Pais ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar Localidad (codigo={l.Id}): {ex.Message}");
            }
        }
    }
}