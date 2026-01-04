import { useState, useEffect } from "react";
import { CategoryType } from "./Categories";
import "./Products.css";


type ProductType = {
    id: number;
    title: string;
    description: string;
    price: number;
    category: CategoryType;
};

type Props = {
    categoryId: number;
};

const Products = ({ categoryId }: Props) => {
    const [products, setProducts] = useState<ProductType[]>([]);

    useEffect(() => {
        const loadProducts = async () => {
            const productResult = await (
                await fetch("https://localhost:7012/Product")
            ).json();

            setProducts(
                productResult?.filter((x: ProductType) => x.category?.id === categoryId)
            );
        };

        loadProducts();
    }, [categoryId]);

    return (
        <div className="products">
            {products.map((product) => {
                return (
                    <div className="product-box" key={product.id}>
                        <div>{product.title}</div>
                        <div>{product.description}</div>
                    </div>
                );
            })}
        </div>
    );
};

export default Products;
