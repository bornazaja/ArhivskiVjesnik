using ArhivskiVjesnikLibrary.BLL.DTO;
using ArhivskiVjesnikLibrary.BLL.Services.Interfaces;
using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using System;
using System.Collections.Generic;

namespace ArhivskiVjesnikConsole
{
    public class Application : IApplication
    {
        private IClanakService _clanakService;
        private IAutorService _autorService;

        public Application(IClanakService clanakService, IAutorService autorService)
        {
            _clanakService = clanakService;
            _autorService = autorService;
        }

        public void Run()
        {
            try
            {
                ShowClanci();
                ShowAutori();
            }
            catch (Exception)
            {
                Console.WriteLine("Desila se greška.");
            }
        }

        private QueryCriteria PrepareQueryCriteria()
        {
            return new QueryCriteria
            {
                SearchCriterias = PrepareSearchCriterias(),
                Operator = Operator.Or,
                SortCriteria = PrepareSortCriteria(),
                PageCriteria = PreparePageCriteria()
            };
        }

        private List<SearchCriteria> PrepareSearchCriterias()
        {
            return new List<SearchCriteria>
            {
                new SearchCriteria
                {
                    SearchCriteriaType = SearchCriteriaType.Include,
                    ColumnName = "Clanak.Godiste",
                    Term = "2003",
                    SearchOperation = SearchOperation.GreaterThanOrEqualTo
                },
                new SearchCriteria
                {
                    SearchCriteriaType = SearchCriteriaType.Include,
                    ColumnName = "Autor.Prezime",
                    Term = "lajnert",
                    SearchOperation = SearchOperation.Equal
                },
                new SearchCriteria
                {
                    SearchCriteriaType = SearchCriteriaType.Include,
                    ColumnName = "Autor.Prezime",
                    Term = "janjatović",
                    SearchOperation = SearchOperation.Equal
                }
            };
        }

        private SortCriteria PrepareSortCriteria()
        {
            return new SortCriteria
            {
                ColumnName = "Clanak.IDClanak",
                SortDirection = SortDirection.Ascending
            };
        }

        private PageCriteria PreparePageCriteria()
        {
            return new PageCriteria
            {
                Page = 1,
                Size = 50
            };
        }

        private void ShowClanci()
        {
            PagedList<ClanakDto> clanakPagedList = _clanakService.GetAll(PrepareQueryCriteria());

            Console.WriteLine("CLANCI");
            foreach (var clanak in clanakPagedList.Subset)
            {
                Console.WriteLine($"{clanak.IDClanak}\t{clanak.Naziv}\t{clanak.Godiste}\t{clanak.Volumen}\t{clanak.Broj}\t{clanak.DatumIzdavanja}\t{clanak.DatumObjave}\t{clanak.URL}");
            }

            Console.WriteLine($"\nBroj clanaka: {clanakPagedList.TotalCount}");
            Console.WriteLine($"Broj stranice: {clanakPagedList.PageCriteria.Page} / {clanakPagedList.TotalPages}");
        }

        private void ShowAutori()
        {
            IEnumerable<string> autori = _autorService.GetAll("Autor.Ime+Autor.Prezime", "lajnert");
            Console.WriteLine("\n\nAUTORI BY SEARCH");
            foreach (var autor in autori)
            {
                Console.WriteLine(autor);
            }

            PagedList<AutorDto> pagedList = _autorService.GetAllByClanakID(1, PreparePageCriteria());
            Console.WriteLine("\n\nAUTORI BY CLANAK ID");
            foreach (var autor in pagedList.Subset)
            {
                Console.WriteLine($"{autor.IDAutor}\t{autor.Ime}\t{autor.Prezime}");
            }
        }
    }
}
