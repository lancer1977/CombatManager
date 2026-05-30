using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class SQLiteAndViewerSmokeTests
    {
        [Test]
        public void DetailsDb_contains_the_seed_tables_used_by_the_persistence_workflow()
        {
            var dbPath = GetRepoPath("CombatManagerCore", "Details.db");

            Assert.That(File.Exists(dbPath), Is.True, "Expected the repository SQLite seed database to exist.");

            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            AssertTableHasRows(connection, "Bestiary");
            AssertTableHasRows(connection, "MagicItems");
            AssertTableHasRows(connection, "Rules");
            AssertTableHasRows(connection, "Spells");
        }

        [Test]
        public void CombatStateService_contract_exposes_the_refresh_callbacks_consumed_by_the_viewer()
        {
            var contract = ReadSource("CombatViewService", "ICombatStateService.cs");

            Assert.That(contract, Does.Contain("[ServiceContract(CallbackContract = typeof(ICombatStateCallback))]"));
            Assert.That(contract, Does.Contain("void CurrentPlayerChanged(Guid id);"));
            Assert.That(contract, Does.Contain("void CombatListChanged();"));
            Assert.That(contract, Does.Contain("void CharactersChanged();"));
        }

        [Test]
        public void CombatStateViewer_main_window_refreshes_the_active_character_and_cached_lists_from_callbacks()
        {
            var viewer = ReadSource("CombatStateViewer", "MainWindow.xaml.cs");

            Assert.That(viewer, Does.Contain("public void CurrentPlayerChanged(Guid id)"));
            Assert.That(viewer, Does.Contain("Dispatcher.BeginInvoke(new GuidDelegate(CurrentPlayerChangedInvoke)"));
            Assert.That(viewer, Does.Contain("ActiveCharacterBorder.DataContext = _Characters.FirstOrDefault(a => a.ID == id);"));
            Assert.That(viewer, Does.Contain("public void CombatListChanged()"));
            Assert.That(viewer, Does.Contain("serviceClient.GetCombatList();"));
            Assert.That(viewer, Does.Contain("public void CharactersChanged()"));
            Assert.That(viewer, Does.Contain("serviceClient.GetCharacters()"));
        }

        private static void AssertTableHasRows(SqliteConnection connection, string tableName)
        {
            using var command = connection.CreateCommand();
            command.CommandText = $"select count(*) from {tableName}";
            var count = Convert.ToInt32(command.ExecuteScalar());

            Assert.That(count, Is.GreaterThan(0), $"Expected {tableName} to contain persisted rows.");
        }

        private static string ReadSource(params string[] relativePathParts)
        {
            return File.ReadAllText(GetRepoPath(relativePathParts));
        }

        private static string GetRepoPath(params string[] relativePathParts)
        {
            var repoRoot = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", ".."));
            return Path.Combine(new[] { repoRoot }.Concat(relativePathParts).ToArray());
        }
    }
}
