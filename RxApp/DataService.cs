using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RxApp
{
    public class DataService
    {
        private string[] _data = new[]
        {
            "In Search of Lost Time by Marcel Proust",
            "Ulysses by James Joyce",
            "Don Quixote by Miguel de Cervantes",
            "One Hundred Years of Solitude by Gabriel Garcia Marquez",
            "The Great Gatsby by F. Scott Fitzgerald",
            "Moby Dick by Herman Melville",
            "War and Peace by Leo Tolstoy",
            "Hamlet by William Shakespeare",
            "The Odyssey by Homer",
            "Madame Bovary by Gustave Flaubert",
            "The Divine Comedy by Dante Alighieri",
            "Lolita by Vladimir Nabokov",
            "The Brothers Karamazov by Fyodor Dostoyevsky",
            "Crime and Punishment by Fyodor Dostoyevsky",
            "Wuthering Heights by Emily Brontë",
            "The Catcher in the Rye by J. D. Salinger",
            "Pride and Prejudice by Jane Austen",
            "The Adventures of Huckleberry Finn by Mark Twain",
            "Anna Karenina by Leo Tolstoy",
            "Alice's Adventures in Wonderland by Lewis Carroll",
            "The Iliad by Homer",
            "To the Lighthouse by Virginia Woolf",
            "Catch-22 by Joseph Heller",
            "Heart of Darkness by Joseph Conrad",
            "The Sound and the Fury by William Faulkner",
            "Nineteen Eighty Four by George Orwell",
            "Great Expectations by Charles Dickens",
            "One Thousand and One Nights by India/Iran/Iraq/Egypt",
            "The Grapes of Wrath by John Steinbeck",
            "Absalom, Absalom! by William Faulkner",
            "Invisible Man by Ralph Ellison",
            "To Kill a Mockingbird by Harper Lee",
            "The Trial by Franz Kafka",
            "The Red and the Black by Stendhal",
            "Middlemarch by George Eliot",
            "Gulliver's Travels by Jonathan Swift",
            "Beloved by Toni Morrison",
            "Mrs. Dalloway by Virginia Woolf",
            "The Stories of Anton Chekhov by Anton Chekhov",
            "The Stranger by Albert Camus",
            "Jane Eyre by Charlotte Bronte",
            "The Aeneid by Virgil",
            "Collected Fiction by Jorge Luis Borges",
            "The Sun Also Rises by Ernest Hemingway",
            "David Copperfield by Charles Dickens",
            "Tristram Shandy by Laurence Sterne",
            "Leaves of Grass by Walt Whitman",
            "The Magic Mountain by Thomas Mann",
            "A Portrait of the Artist as a Young Man by James Joyce",
            "Midnight's Children by Salman Rushdie"
        };
        public IEnumerable<string> Search(string term)
        {
            var r = new Regex(term);
            var results = new List<string>();
            System.Threading.Thread.Sleep(2000);
            foreach (var entry in _data)
            {
                if (r.IsMatch(entry))
                {
                    results.Add(entry);
                }
            }            

            return results;
        }

        public IObservable<string> SearchO(string term)
        {
            var r = new Regex(term);
            var results = new List<string>();
            System.Threading.Thread.Sleep(2000);
            foreach (var entry in _data)
            {
                if (r.IsMatch(entry))
                {
                    results.Add(entry);
                }
            }

            return results.ToObservable();
        }

        public async Task<IEnumerable<string>> SearchAsync(string term)
        {
            Trace.WriteLine("Searching");
            var r = new Regex(term);
            var results = new List<string>();
            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(2000);
                foreach (var entry in _data)
                {
                    if (r.IsMatch(entry))
                    {
                        results.Add(entry);
                    }
                }

            }).ConfigureAwait(false);
            return results;
        }

        public async IAsyncEnumerable<string> SearchStream(string term)
        {
            Trace.WriteLine("Searching");
            var r = new Regex(term);
            foreach (var entry in _data)
            {
                if (r.IsMatch(entry))
                {
                    await Task.Delay(100).ConfigureAwait(false);
                    yield return entry;
                }
            }
        }
    }
}
