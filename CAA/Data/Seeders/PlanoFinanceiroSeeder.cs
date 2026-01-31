using CAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CAA.Data.Seeders
{
    public static class PlanoFinanceiroSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Buscar tipos de desconto (fora dos blocos de plano)
            var tipoDescontoPontualidade = await context.TipoDesconto.FirstOrDefaultAsync(t => t.Nome == "Pontualidade");
            if (tipoDescontoPontualidade == null)
            {
                tipoDescontoPontualidade = new TipoDesconto { Nome = "Pontualidade" };
                context.TipoDesconto.Add(tipoDescontoPontualidade);
                await context.SaveChangesAsync();
            }

            // Seed para Comércio Exterior - Plano Noturno Normal com descontos
            var cursoComExt = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Comércio Exterior");
            if (cursoComExt != null)
            {
                var planoComExtNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoComExt.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoComExtNoturnoNormal == null)
                {
                    planoComExtNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoComExt.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoComExtNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoComExtPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoComExtNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoComExtPontualidade == null)
                {
                    descontoComExtPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoComExtNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoComExtPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Fotografia - Plano Noturno Normal com descontos
            var cursoFotografia = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Fotografia");
            if (cursoFotografia != null)
            {
                var planoFotografiaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoFotografia.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoFotografiaNoturnoNormal == null)
                {
                    planoFotografiaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoFotografia.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoFotografiaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoFotografiaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoFotografiaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoFotografiaPontualidade == null)
                {
                    descontoFotografiaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoFotografiaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoFotografiaPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Gestão Comercial - Plano Noturno Normal com descontos
            var cursoGestaoComercial = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Gestão Comercial");
            if (cursoGestaoComercial != null)
            {
                var planoGestaoComercialNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoGestaoComercial.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoGestaoComercialNoturnoNormal == null)
                {
                    planoGestaoComercialNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoGestaoComercial.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoGestaoComercialNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoGestaoComercialPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoGestaoComercialNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoGestaoComercialPontualidade == null)
                {
                    descontoGestaoComercialPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoGestaoComercialNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoGestaoComercialPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Gestão da Qualidade - Plano Noturno Normal com descontos
            var cursoGestaoQualidade = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Gestão da Qualidade");
            if (cursoGestaoQualidade != null)
            {
                var planoGestaoQualidadeNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoGestaoQualidade.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoGestaoQualidadeNoturnoNormal == null)
                {
                    planoGestaoQualidadeNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoGestaoQualidade.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoGestaoQualidadeNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoGestaoQualidadePontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoGestaoQualidadeNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoGestaoQualidadePontualidade == null)
                {
                    descontoGestaoQualidadePontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoGestaoQualidadeNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoGestaoQualidadePontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Gestão de Marketing - Plano Noturno Normal com descontos
            var cursoGestaoMarketing = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Gestão de Marketing");
            if (cursoGestaoMarketing != null)
            {
                var planoGestaoMarketingNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoGestaoMarketing.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoGestaoMarketingNoturnoNormal == null)
                {
                    planoGestaoMarketingNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoGestaoMarketing.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoGestaoMarketingNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoGestaoMarketingPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoGestaoMarketingNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoGestaoMarketingPontualidade == null)
                {
                    descontoGestaoMarketingPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoGestaoMarketingNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoGestaoMarketingPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Gestão de Recursos Humanos - Plano Noturno Normal com descontos
            var cursoGestaoRecursosHumanos = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Gestão de Recursos Humanos");
            if (cursoGestaoRecursosHumanos != null)
            {
                var planoGestaoRecursosHumanosNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoGestaoRecursosHumanos.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoGestaoRecursosHumanosNoturnoNormal == null)
                {
                    planoGestaoRecursosHumanosNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoGestaoRecursosHumanos.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoGestaoRecursosHumanosNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoGestaoRecursosHumanosPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoGestaoRecursosHumanosNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoGestaoRecursosHumanosPontualidade == null)
                {
                    descontoGestaoRecursosHumanosPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoGestaoRecursosHumanosNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoGestaoRecursosHumanosPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Adicione aqui seeds para outros cursos e planos conforme necessário

            // Seed para Gestão Financeira - Plano Noturno Normal com descontos
            var cursoGestaoFinanceira = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Gestão Financeira");
            if (cursoGestaoFinanceira != null)
            {
                var planoGestaoFinanceiraNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoGestaoFinanceira.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoGestaoFinanceiraNoturnoNormal == null)
                {
                    planoGestaoFinanceiraNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoGestaoFinanceira.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoGestaoFinanceiraNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoGestaoFinanceiraPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoGestaoFinanceiraNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoGestaoFinanceiraPontualidade == null)
                {
                    descontoGestaoFinanceiraPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoGestaoFinanceiraNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoGestaoFinanceiraPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Logística - Plano Noturno Normal com descontos
            var cursoLogistica = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Logística");
            if (cursoLogistica != null)
            {
                var planoLogisticaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoLogistica.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoLogisticaNoturnoNormal == null)
                {
                    planoLogisticaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoLogistica.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoLogisticaNoturnoNormal);
                    await context.SaveChangesAsync();
                }


                // Desconto Pontualidade
                var descontoLogisticaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoLogisticaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoLogisticaPontualidade == null)
                {
                    descontoLogisticaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoLogisticaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoLogisticaPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Processos Gerenciais - Plano Noturno Normal com descontos
            var cursoProcessosGerenciais = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Processos Gerenciais");
            if (cursoProcessosGerenciais != null)
            {
                var planoProcessosGerenciaisNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoProcessosGerenciais.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoProcessosGerenciaisNoturnoNormal == null)
                {
                    planoProcessosGerenciaisNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoProcessosGerenciais.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoProcessosGerenciaisNoturnoNormal);
                    await context.SaveChangesAsync();
                }


                // Desconto Pontualidade
                var descontoProcessosGerenciaisPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoProcessosGerenciaisNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoProcessosGerenciaisPontualidade == null)
                {
                    descontoProcessosGerenciaisPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoProcessosGerenciaisNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoProcessosGerenciaisPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Produção Audiovisual - Plano Noturno Normal com descontos
            var cursoProducaoAudiovisual = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Produção Audiovisual");
            if (cursoProducaoAudiovisual != null)
            {
                var planoProducaoAudiovisualNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoProducaoAudiovisual.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoProducaoAudiovisualNoturnoNormal == null)
                {
                    planoProducaoAudiovisualNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoProducaoAudiovisual.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoProducaoAudiovisualNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoProducaoAudiovisualPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoProducaoAudiovisualNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoProducaoAudiovisualPontualidade == null)
                {
                    descontoProducaoAudiovisualPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoProducaoAudiovisualNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoProducaoAudiovisualPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Produção Fonográfica - Plano Noturno Normal com descontos
            var cursoProducaoFonografica = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Produção Fonográfica");
            if (cursoProducaoFonografica != null)
            {
                var planoProducaoFonograficaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoProducaoFonografica.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoProducaoFonograficaNoturnoNormal == null)
                {
                    planoProducaoFonograficaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoProducaoFonografica.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 972.00M, // Ajuste o valor conforme necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoProducaoFonograficaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoProducaoFonograficaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoProducaoFonograficaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoProducaoFonograficaPontualidade == null)
                {
                    descontoProducaoFonograficaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoProducaoFonograficaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoProducaoFonograficaPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Análise e Desenvolvimento de Sistemas - Plano Noturno Normal com descontos específicos
            var cursoADS = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Análise e Desenvolvimento de Sistemas");
            if (cursoADS != null)
            {
                var planoADSNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoADS.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoADSNoturnoNormal == null)
                {
                    planoADSNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoADS.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1596.40M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoADSNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoADSPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoADSNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoADSPontualidade == null)
                {
                    descontoADSPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoADSNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoADSPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Design de Moda Tecnólogo - Plano Noturno Normal com descontos específicos
            var cursoDesignModaTecnologo = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Design de Moda" && c.Titulacao == Titulacao.Tecnologo);
            if (cursoDesignModaTecnologo != null)
            {
                var planoDesignModaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDesignModaTecnologo.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDesignModaNoturnoNormal == null)
                {
                    planoDesignModaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDesignModaTecnologo.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1277.86M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoDesignModaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDesignModaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDesignModaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDesignModaPontualidade == null)
                {
                    descontoDesignModaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDesignModaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDesignModaPontualidade);
                }
                await context.SaveChangesAsync();

                // Seed para Design de Moda Tecnólogo - Plano Matutino Normal com descontos específicos
                var planoDesignModaMatutinoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDesignModaTecnologo.CursoId && p.Matutino && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDesignModaMatutinoNormal == null)
                {
                    planoDesignModaMatutinoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDesignModaTecnologo.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1357.72M,
                        Matutino = true,
                        Noturno = false
                    };
                    context.PlanoFinanceiro.Add(planoDesignModaMatutinoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDesignModaMatutinoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDesignModaMatutinoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDesignModaMatutinoPontualidade == null)
                {
                    descontoDesignModaMatutinoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDesignModaMatutinoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDesignModaMatutinoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Estética - Plano Noturno Normal com desconto de pontualidade
            var cursoEstetica = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Estética e Cosmética");
            if (cursoEstetica != null)
            {
                var planoEsteticaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoEstetica.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoEsteticaNoturnoNormal == null)
                {
                    planoEsteticaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoEstetica.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1277.86M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoEsteticaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoEsteticaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoEsteticaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoEsteticaPontualidade == null)
                {
                    descontoEsteticaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoEsteticaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoEsteticaPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Direito - Plano Noturno Normal com descontos específicos
            var cursoDireito = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Direito" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoDireito != null)
            {
                var planoDireitoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDireito.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDireitoNoturnoNormal == null)
                {
                    planoDireitoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDireito.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 2079.04M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoDireitoNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDireitoNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDireitoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDireitoNoturnoPontualidade == null)
                {
                    descontoDireitoNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDireitoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDireitoNoturnoPontualidade);
                }
                await context.SaveChangesAsync();

                // Seed para Direito - Plano Matutino Normal com descontos específicos
                var planoDireitoMatutinoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDireito.CursoId && p.Matutino && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDireitoMatutinoNormal == null)
                {
                    planoDireitoMatutinoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDireito.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 2310.04M,
                        Matutino = true,
                        Noturno = false
                    };
                    context.PlanoFinanceiro.Add(planoDireitoMatutinoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDireitoMatutinoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDireitoMatutinoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDireitoMatutinoPontualidade == null)
                {
                    descontoDireitoMatutinoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDireitoMatutinoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDireitoMatutinoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Arquitetura e Urbanismo - Plano Noturno Normal com descontos específicos
            var cursoArquitetura = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Arquitetura e Urbanismo" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoArquitetura != null)
            {
                var planoArquiteturaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoArquitetura.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoArquiteturaNoturnoNormal == null)
                {
                    planoArquiteturaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoArquitetura.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M, // ajuste o valor se necessário
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoArquiteturaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoArquiteturaNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoArquiteturaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoArquiteturaNoturnoPontualidade == null)
                {
                    descontoArquiteturaNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoArquiteturaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoArquiteturaNoturnoPontualidade);
                }
                await context.SaveChangesAsync();

                // Seed para Arquitetura e Urbanismo - Plano Matutino Normal com descontos específicos
                var planoArquiteturaMatutinoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoArquitetura.CursoId && p.Matutino && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoArquiteturaMatutinoNormal == null)
                {
                    planoArquiteturaMatutinoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoArquitetura.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 2336.53M, // ajuste o valor se necessário
                        Matutino = true,
                        Noturno = false
                    };
                    context.PlanoFinanceiro.Add(planoArquiteturaMatutinoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoArquiteturaMatutinoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoArquiteturaMatutinoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoArquiteturaMatutinoPontualidade == null)
                {
                    descontoArquiteturaMatutinoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoArquiteturaMatutinoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoArquiteturaMatutinoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Administração - Plano Noturno Normal com desconto de pontualidade
            var cursoAdministracao = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Administração" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoAdministracao != null)
            {
                var planoAdministracaoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoAdministracao.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoAdministracaoNoturnoNormal == null)
                {
                    planoAdministracaoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoAdministracao.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoAdministracaoNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoAdministracaoNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoAdministracaoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoAdministracaoNoturnoPontualidade == null)
                {
                    descontoAdministracaoNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoAdministracaoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoAdministracaoNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Ciências Econômicas - Plano Noturno Normal com desconto de pontualidade
            var cursoCienciasEconomicas = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Ciências Econômicas" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoCienciasEconomicas != null)
            {
                var planoCienciasEconomicasNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoCienciasEconomicas.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoCienciasEconomicasNoturnoNormal == null)
                {
                    planoCienciasEconomicasNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoCienciasEconomicas.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoCienciasEconomicasNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoCienciasEconomicasNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoCienciasEconomicasNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoCienciasEconomicasNoturnoPontualidade == null)
                {
                    descontoCienciasEconomicasNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoCienciasEconomicasNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoCienciasEconomicasNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Relações Internacionais - Plano Noturno Normal com desconto de pontualidade
            var cursoRelacoesInternacionais = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Relações Internacionais" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoRelacoesInternacionais != null)
            {
                var planoRelacoesInternacionaisNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoRelacoesInternacionais.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoRelacoesInternacionaisNoturnoNormal == null)
                {
                    planoRelacoesInternacionaisNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoRelacoesInternacionais.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoRelacoesInternacionaisNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoRelacoesInternacionaisNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoRelacoesInternacionaisNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoRelacoesInternacionaisNoturnoPontualidade == null)
                {
                    descontoRelacoesInternacionaisNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoRelacoesInternacionaisNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoRelacoesInternacionaisNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Publicidade e Propaganda - Plano Noturno Normal com desconto de pontualidade
            var cursoPublicidadePropaganda = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Publicidade e Propaganda" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoPublicidadePropaganda != null)
            {
                var planoPublicidadePropagandaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoPublicidadePropaganda.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoPublicidadePropagandaNoturnoNormal == null)
                {
                    planoPublicidadePropagandaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoPublicidadePropaganda.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoPublicidadePropagandaNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoPublicidadePropagandaNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoPublicidadePropagandaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoPublicidadePropagandaNoturnoPontualidade == null)
                {
                    descontoPublicidadePropagandaNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoPublicidadePropagandaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoPublicidadePropagandaNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Jornalismo - Plano Noturno Normal com desconto de pontualidade
            var cursoJornalismo = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Jornalismo" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoJornalismo != null)
            {
                var planoJornalismoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoJornalismo.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoJornalismoNoturnoNormal == null)
                {
                    planoJornalismoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoJornalismo.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoJornalismoNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoJornalismoNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoJornalismoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoJornalismoNoturnoPontualidade == null)
                {
                    descontoJornalismoNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoJornalismoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoJornalismoNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Design Gráfico - Plano Noturno Normal com desconto de pontualidade
            var cursoDesignGrafico = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Design – Comunicação visual" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoDesignGrafico != null)
            {
                var planoDesignGraficoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDesignGrafico.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDesignGraficoNoturnoNormal == null)
                {
                    planoDesignGraficoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDesignGrafico.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoDesignGraficoNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDesignGraficoNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDesignGraficoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDesignGraficoNoturnoPontualidade == null)
                {
                    descontoDesignGraficoNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDesignGraficoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDesignGraficoNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Design de Moda Bacharelado - Plano Noturno Normal com descontos específicos
            var cursoDesignModaBacharelado = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Design de Moda" && c.Titulacao == Titulacao.Bacharelado);
            if (cursoDesignModaBacharelado != null)
            {
                var planoDesignModaBachareladoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoDesignModaBacharelado.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoDesignModaBachareladoNoturnoNormal == null)
                {
                    planoDesignModaBachareladoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoDesignModaBacharelado.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1835.84M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoDesignModaBachareladoNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoDesignModaBachareladoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoDesignModaBachareladoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoDesignModaBachareladoPontualidade == null)
                {
                    descontoDesignModaBachareladoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoDesignModaBachareladoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoDesignModaBachareladoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Ciências Contábeis - Plano Noturno Normal com desconto de pontualidade
            var cursoCienciasContabeis = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Ciências Contábeis");
            if (cursoCienciasContabeis != null)
            {
                var planoCienciasContabeisNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoCienciasContabeis.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoCienciasContabeisNoturnoNormal == null)
                {
                    planoCienciasContabeisNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoCienciasContabeis.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1475.68M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoCienciasContabeisNoturnoNormal);
                    await context.SaveChangesAsync();
                }

                // Desconto Pontualidade
                var descontoCienciasContabeisNoturnoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoCienciasContabeisNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoCienciasContabeisNoturnoPontualidade == null)
                {
                    descontoCienciasContabeisNoturnoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoCienciasContabeisNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoCienciasContabeisNoturnoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Engenharia Civil - Plano Noturno Normal com desconto de pontualidade
            var cursoEngenhariaCivil = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Engenharia Civil");
            if (cursoEngenhariaCivil != null)
            {
                var planoEngenhariaCivilNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoEngenhariaCivil.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoEngenhariaCivilNoturnoNormal == null)
                {
                    planoEngenhariaCivilNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoEngenhariaCivil.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1708.15M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoEngenhariaCivilNoturnoNormal);
                    await context.SaveChangesAsync();
                }
                var descontoEngenhariaCivilPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoEngenhariaCivilNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoEngenhariaCivilPontualidade == null)
                {
                    descontoEngenhariaCivilPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoEngenhariaCivilNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoEngenhariaCivilPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Engenharia de Produção - Plano Noturno Normal com desconto de pontualidade
            var cursoEngenhariaProducao = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Engenharia de Produção");
            if (cursoEngenhariaProducao != null)
            {
                var planoEngenhariaProducaoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoEngenhariaProducao.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoEngenhariaProducaoNoturnoNormal == null)
                {
                    planoEngenhariaProducaoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoEngenhariaProducao.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1708.15M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoEngenhariaProducaoNoturnoNormal);
                    await context.SaveChangesAsync();
                }
                var descontoEngenhariaProducaoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoEngenhariaProducaoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoEngenhariaProducaoPontualidade == null)
                {
                    descontoEngenhariaProducaoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoEngenhariaProducaoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoEngenhariaProducaoPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Engenharia Mecânica - Plano Noturno Normal com desconto de pontualidade
            var cursoEngenhariaMecanica = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Engenharia Mecânica");
            if (cursoEngenhariaMecanica != null)
            {
                var planoEngenhariaMecanicaNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoEngenhariaMecanica.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoEngenhariaMecanicaNoturnoNormal == null)
                {
                    planoEngenhariaMecanicaNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoEngenhariaMecanica.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1708.15M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoEngenhariaMecanicaNoturnoNormal);
                    await context.SaveChangesAsync();
                }
                var descontoEngenhariaMecanicaPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoEngenhariaMecanicaNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoEngenhariaMecanicaPontualidade == null)
                {
                    descontoEngenhariaMecanicaPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoEngenhariaMecanicaNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoEngenhariaMecanicaPontualidade);
                }
                await context.SaveChangesAsync();
            }

            // Seed para Engenharia de Computação - Plano Noturno Normal com desconto de pontualidade
            var cursoEngenhariaComputacao = await context.Curso.FirstOrDefaultAsync(c => c.Nome == "Engenharia de Computação");
            if (cursoEngenhariaComputacao != null)
            {
                var planoEngenhariaComputacaoNoturnoNormal = await context.PlanoFinanceiro.FirstOrDefaultAsync(
                    p => p.CursoId == cursoEngenhariaComputacao.CursoId && p.Noturno && p.TipoPlanoFinanceiro == TipoPlanoFinanceiro.PlanoNormal);
                if (planoEngenhariaComputacaoNoturnoNormal == null)
                {
                    planoEngenhariaComputacaoNoturnoNormal = new PlanoFinanceiro
                    {
                        CursoId = cursoEngenhariaComputacao.CursoId,
                        TipoPlanoFinanceiro = TipoPlanoFinanceiro.PlanoNormal,
                        Valor = 1708.15M,
                        Matutino = false,
                        Noturno = true
                    };
                    context.PlanoFinanceiro.Add(planoEngenhariaComputacaoNoturnoNormal);
                    await context.SaveChangesAsync();
                }
                var descontoEngenhariaComputacaoPontualidade = await context.Desconto.FirstOrDefaultAsync(
                    d => d.PlanoFinanceiroId == planoEngenhariaComputacaoNoturnoNormal.PlanoFinanceiroId && d.TipoDescontoId == tipoDescontoPontualidade.TipoDescontoId);
                if (descontoEngenhariaComputacaoPontualidade == null)
                {
                    descontoEngenhariaComputacaoPontualidade = new Desconto
                    {
                        PlanoFinanceiroId = planoEngenhariaComputacaoNoturnoNormal.PlanoFinanceiroId,
                        TipoDescontoId = tipoDescontoPontualidade.TipoDescontoId,
                        TipoDescontoValor = TipoDescontoValor.Porcentagem,
                        Valor = 15.00M
                    };
                    context.Desconto.Add(descontoEngenhariaComputacaoPontualidade);
                }
                await context.SaveChangesAsync();
            }

        }
    }
}
