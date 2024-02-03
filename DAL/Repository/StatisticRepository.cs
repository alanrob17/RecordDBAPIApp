using DAL.Data;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StatisticRepository: IStatisticRepository
    {
        #region " Methods "

        private readonly IDataAccess _db;
        public StatisticRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<Statistic> GetStatistics()
        {
            Statistic statistics = new();

            string query = "select sum(discs) from record where media = 'CD'";
            var result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.TotalCDs = (int?)result ?? 0;

            // query for number of Rock Records
            query = "select sum(discs) from record where field = 'Rock'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.RockDisks = (int?)result ?? 0;

            // query for number of Folk Records
            query = "select sum(discs) from record where field = 'Folk'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.FolkDisks = (int?)result ?? 0;

            // query for number of Acoustic Records
            query = "select sum(discs) from record where field = 'Acoustic'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.AcousticDisks = (int?)result ?? 0;

            // query for number of Jazz and Fusion Records
            query = "select sum(discs) from record where field = 'Jazz' or field = 'Fusion'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.JazzDisks = (int?)result ?? 0;

            // query for number of Blues Records
            query = "select sum(discs) from record where field = 'Blues'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.BluesDisks = (int?)result ?? 0;

            // query for number of Country Records
            query = "select sum(discs) from record where field = 'Country'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.CountryDisks = (int?)result ?? 0;

            // query for number of Classical Records
            query = "select sum(discs) from record where field = 'Classical'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.ClassicalDisks = (int?)result ?? 0;

            // query for number of Soundtrack Records
            query = "select sum(discs) from record where field = 'Soundtrack'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.SoundtrackDisks = (int?)result ?? 0;

            // query for number of Four Star Records
            query = "select count(rating) from record where Rating = '****'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.FourStarDisks = (int?)result ?? 0;

            // query for number of Three Star Records
            query = "select count(rating) from record where Rating = '***'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.ThreeStarDisks = (int?)result ?? 0;

            // query for number of Two Star Records
            query = "select count(rating) from record where Rating = '**'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.TwoStarDisks = (int?)result ?? 0;

            // query for number of One Star Records
            query = "select count(rating) from record where Rating = '*'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.OneStarDisks = (int?)result ?? 0;

            // query for Number of records
            query = "select sum(discs) from record where media='R'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.TotalRecords = (int?)result ?? 0;

            // query for amount spent on records
            query = "select sum(cost) from record where media = 'R'";
            decimal cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.RecordCost = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // query for amount spent on CD's               
            query = "select sum(cost) from record where media = 'CD'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.CDCost = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // calculate the average cost of all CDs
            decimal avCdCost = statistics.CDCost / (decimal)statistics.TotalCDs;
            statistics.AvCDCost = Math.Round(avCdCost, 2);

            // query for amount spent on records and CD's
            query = "select sum(cost) from record";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.TotalCost = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // query for Number of CD's bought in 2017               
            query = "select sum(discs) from record where bought > '31-Dec-2016' and bought < '01-Jan-2018'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2017 = (int?)result ?? 0;

            // query for amount spent on CD's in 2017
            query = "select sum(cost) from record where bought > '31-Dec-2016' and bought < '01-Jan-2018'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2017 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2017 > 1)
            {
                var av2017 = statistics.Cost2017 / (decimal)statistics.Disks2017;
                statistics.Av2017 = Math.Round(av2017, 2);
            }
            else
            {
                statistics.Cost2017 = 0.00m;
                statistics.Av2017 = 0.00m;
            }

            // query for Number of CD's bought in 2018               
            query = "select sum(discs) from record where bought > '31-Dec-2017' and bought < '01-Jan-2019'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2018 = (int?)result ?? 0;

            // query for amount spent on CD's in 2018
            query = "select sum(cost) from record where bought > '31-Dec-2017' and bought < '01-Jan-2019'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2018 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2018 > 1)
            {
                var av2018 = statistics.Cost2018 / (decimal)statistics.Disks2018;
                statistics.Av2018 = Math.Round(av2018, 2);
            }
            else
            {
                statistics.Cost2018 = 0.00m;
                statistics.Av2018 = 0.00m;
            }

            // query for Number of CD's bought in 2019
            query = "select sum(discs) from record where bought > '31-Dec-2018' and bought < '01-Jan-2020'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2019 = (int?)result ?? 0;

            // query for amount spent on CD's in 2019
            query = "select sum(cost) from record where bought > '31-Dec-2018' and bought < '01-Jan-2020'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2019 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2019 > 1)
            {
                var av2019 = statistics.Cost2019 / (decimal)statistics.Disks2019;
                statistics.Av2019 = Math.Round(av2019, 2);
            }
            else
            {
                statistics.Cost2019 = 0.00m;
                statistics.Av2019 = 0.00m;
            }

            // query for Number of CD's bought in 2020
            query = "select sum(discs) from record where bought > '31-Dec-2019' and bought < '01-Jan-2021'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2020 = (int?)result ?? 0;

            // query for amount spent on CD's in 2020
            query = "select sum(cost) from record where bought > '31-Dec-2019' and bought < '01-Jan-2021'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2020 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2020 > 1)
            {
                var av2020 = statistics.Cost2020 / (decimal)statistics.Disks2020;
                statistics.Av2020 = Math.Round(av2020, 2);
            }
            else
            {
                statistics.Cost2020 = 0.00m;
                statistics.Av2020 = 0.00m;
            }

            // query for Number of CD's bought in 2021
            query = "select sum(discs) from record where bought > '31-Dec-2020' and bought < '01-Jan-2022'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2021 = (int?)result ?? 0;

            // query for amount spent on CD's in 2021
            query = "select sum(cost) from record where bought > '31-Dec-2020' and bought < '01-Jan-2022'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2021 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2021 > 1)
            {
                var av2021 = statistics.Cost2021 / (decimal)statistics.Disks2021;
                statistics.Av2021 = Math.Round(av2021, 2);
            }
            else
            {
                statistics.Cost2021 = 0.00m;
                statistics.Av2021 = 0.00m;
            }

            // query for Number of CD's bought in 2022
            query = "select sum(discs) from record where bought > '31-Dec-2021' and bought < '01-Jan-2023'";
            result = await _db.GetCountOrIdQ<dynamic>(query, new { });
            statistics.Disks2022 = (int?)result ?? 0;

            // query for amount spent on CD's in 2022
            query = "select sum(cost) from record where bought > '31-Dec-2021' and bought < '01-Jan-2023'";
            cost = await _db.GetCostQ<dynamic>(query, new { });
            statistics.Cost2022 = (decimal?)Math.Round(cost, 2) ?? 0.00m;

            // this is to stop a divide by zero error if nothing has been bought
            if (statistics.Cost2022 > 1)
            {
                var av2022 = statistics.Cost2022 / (decimal)statistics.Disks2022;
                statistics.Av2022 = Math.Round(av2022, 2);
            }
            else
            {
                statistics.Cost2022 = 0.00m;
                statistics.Av2022 = 0.00m;
            }

            return statistics;
        }

        #endregion
    }
}
