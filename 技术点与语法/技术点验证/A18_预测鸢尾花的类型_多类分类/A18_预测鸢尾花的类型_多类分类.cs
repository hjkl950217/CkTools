using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace 技术点验证
{
    //需要 Microsoft.ML
    //可以根据花瓣长度等特征预测鸢尾花的类型
    //Iris Setosa(山鸢尾)、Iris Versicolour(杂色鸢尾),Iris Virginica(维吉尼亚鸢尾)

    /*
     * 简书文档1：http://blog.lisp4fun.com/2018/03/03/decision-tree
     *
     * 微软文档1：https://dotnet.microsoft.com/learn/machinelearning-ai/ml-dotnet-get-started-tutorial/intro
     * 微软文档2：https://dotnet.microsoft.com/learn/machinelearning-ai/ml-dotnet-get-started-tutorial/code
     *
     * 知乎资料：https://zhuanlan.zhihu.com/p/45955585
     * 最大熵模型资料：https://www.cnblogs.com/pinard/p/6093948.html
     *
     *
     */

    [VerifcationType(VerificationTypeEnum.A18_预测鸢尾花的类型_多类分类)]
    public class A18_预测鸢尾花的类型_多类分类 : IVerification
    {
        public const string? dataBaseAddr = @"A18_预测鸢尾花的类型_多类分类\data";
        public const string? _dataPath = dataBaseAddr + @"\iris.txt";

        public void Start(string[]? args)
        {
            //创建ML上下文
            MLContext mlContext = new MLContext();

            //加载数据

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<IrisData>(
                path: _dataPath,
                hasHeader: false,
                separatorChar: ',');

            #region 转换数据并添加学习者--构建模型

            //获取处理管道的第一个节点-值转键估计器
            ValueToKeyMappingEstimator estimator = mlContext.Transforms.Conversion.MapValueToKey("Label");

            //添加转换估计器
            //把这4个特征组合成一个特征向量, 新特征列名叫Features
            ColumnConcatenatingEstimator columnConcatenatingEstimator = mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth");
            EstimatorChain<ColumnConcatenatingTransformer> chain = estimator.Append(columnConcatenatingEstimator);

            //添加检查点
            //后续处理节点将会使用检查点的数据格式
            chain = chain.AppendCacheCheckpoint(mlContext);

            //添加最大熵估计器-添加学习算法
            //这里可以理解为读取到每条数据的Label的值，将这个值处理到Features这个特征列中
            SdcaMaximumEntropyMulticlassTrainer maxEntropyEstimator = mlContext
                .MulticlassClassification
                .Trainers
                .SdcaMaximumEntropy(
                    labelColumnName: "Label",   //这个值是原始实体中的标志列
                    featureColumnName: "Features"); //特征列的列名

            EstimatorChain<MulticlassPredictionTransformer<MaximumEntropyModelParameters>> chain2 = chain.Append(maxEntropyEstimator);

            //添加键转值估计器
            KeyToValueMappingEstimator kToVEstimator = mlContext
                .Transforms
                .Conversion
                .MapKeyToValue("PredictedLabel");
            EstimatorChain<KeyToValueMappingTransformer> chain3 = chain2.Append(kToVEstimator);

            EstimatorChain<KeyToValueMappingTransformer> pipeline = chain3;

            //var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
            // .Append(mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
            // .AppendCacheCheckpoint(mlContext)
            // .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
            // .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            #endregion 转换数据并添加学习者--构建模型

            //训练模型
            TransformerChain<KeyToValueMappingTransformer> model = pipeline.Fit(trainingDataView);

            //预测类型
            PredictionEngine<IrisData, IrisPrediction> predictionEngine = mlContext.Model.CreatePredictionEngine<IrisData, IrisPrediction>(model);
            IrisPrediction prediction = predictionEngine.Predict(new IrisData()
            {
                SepalLength = 3.3f,
                SepalWidth = 1.6f,
                PetalLength = 0.2f,
                PetalWidth = 5.1f,
            });

            Console.WriteLine($"预测结果：{prediction.ToJsonExt()}");
            ; //这个方法需要迁移到验证库中
        }
    }
}